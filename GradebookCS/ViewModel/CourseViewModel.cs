using GradebookCS.Common;
using GradebookCS.DataBase;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel
{
    public class CourseViewModel :BaseINPC
    {
        #region Attributes
        /// <summary>
        /// store the instance of the Course repository
        /// </summary>
        private CourseTable courseRepository = CourseTable.Instance;

        /// <summary>
        /// Stores the instance of the components table repo
        /// </summary>
        private ComponentTable componentRepository = ComponentTable.Instance;

        /// <summary>
        /// Store the selected ComponentViewerViewModel
        /// </summary>
        private ComponentViewModel selectedComponentViewModel;
        #endregion

        #region Command Properties
        /// <summary>
        /// Command to edit the course info like name and letter score ranges
        /// </summary>
        public RelayCommand EditCourseInfoCommand { get; private set; }

        /// <summary>
        /// Commmand to add a new Component
        /// </summary>
        public RelayCommand AddNewComponentCommand { get; private set; }

        /// <summary>
        /// Command to Remove a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> RemoveComponentCommand { get; private set; }

        /// <summary>
        /// Command to Show editing controls like textboxes for a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> EditComponentCommand { get; private set; }

        /// <summary>
        /// Command to hide editing controls like textboxes for a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> SaveComponentChangesCommand { get; private set; }
        #endregion

        #region Propeties
        /// <summary>
        /// The course for the course view model
        /// </summary>
        public Course Course { get; set; } = new Course();

        /// <summary>
        /// A string property indicating the name of the course and the selected component name.
        /// </summary>
        public string CourseAndComponentName
        {
            get
            {
                return selectedComponentViewModel == null ? "N/A" : Course.Name + " - " + selectedComponentViewModel.Component.Name;
            }
        }

        /// <summary>
        /// List to hold all the componentviewmodels of the courseviewmodel/course
        /// </summary>
        public ObservableCollection<ComponentViewModel> ComponentViewModels { get; private set; } = new ObservableCollection<ComponentViewModel>();

        /// <summary>
        /// Boolean indicating whether the list containing all the components (componentViewmodels) is empty
        /// </summary>
        public bool CanShowComponentDetailPanel { get { return ComponentViewModels.Count > 0; } }

        /// <summary>
        /// The selected compomemtViewModel
        /// </summary>
        public ComponentViewModel SelectedComponentViewModel
        {
            get { return selectedComponentViewModel; }
            set
            {
                if (value != selectedComponentViewModel)
                {
                    if (selectedComponentViewModel != null && selectedComponentViewModel.IsInEditMode)
                    {
                        SaveComponentChanges(selectedComponentViewModel);
                    }
                    selectedComponentViewModel = value;
                    onPropertyChanged();
                    onPropertyChanged("CourseAndComponentName");
                }
            }
        }
        #endregion

        #region Constructors
        public CourseViewModel()
        {
            EditCourseInfoCommand = new RelayCommand(EditCourseInfo, () => true);
            AddNewComponentCommand = new RelayCommand(AddNewComponent, () => true);
            RemoveComponentCommand = new RelayParameterCommand<ComponentViewModel>(DeleteComponent, () => true);
            EditComponentCommand = new RelayParameterCommand<ComponentViewModel>(EditComponent, () => true);
            SaveComponentChangesCommand = new RelayParameterCommand<ComponentViewModel>(SaveComponentChanges, () => true);
            LoadComponentsData();
        }

        public CourseViewModel(Course course)
        {
            this.Course = course;
            EditCourseInfoCommand = new RelayCommand(EditCourseInfo, () => true);
            AddNewComponentCommand = new RelayCommand(AddNewComponent, () => true);
            RemoveComponentCommand = new RelayParameterCommand<ComponentViewModel>(DeleteComponent, () => true);
            EditComponentCommand = new RelayParameterCommand<ComponentViewModel>(EditComponent, () => true);
            SaveComponentChangesCommand = new RelayParameterCommand<ComponentViewModel>(SaveComponentChanges, () => true);
            LoadComponentsData();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets the components item from database and adds then to this component lis
        /// </summary>
        public void LoadComponentsData()
        {
            var items = componentRepository.GetAllItemsForId(Course.Id);    //Get all the items for this course
            foreach (Component c in items)                                   //loop through all of them
            {
                ComponentViewModel cvm = new ComponentViewModel(c);             //Create a new componentviewmodel for this component
                if (cvm.AssignmentViewModels.Count > 0)
                    Course.Grade.Add(cvm.Component.WeightedGrade);
                cvm.Component.PropertyChanged += Component_PropertyChanged;
                ComponentViewModels.Add(cvm);                                   //Add the new componentviewmodel to the list.
            }
            ComponentViewModels.CollectionChanged += ComponentViewModels_CollectionChanged;
            if (ComponentViewModels.Count >= 1)
                SelectedComponentViewModel = ComponentViewModels.First();
        }

        private void ComponentViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ComponentViewModel cvm in e.NewItems)
                {
                    //Course.Grade.Add(cvm.Component.WeightedGrade);
                    cvm.Component.PropertyChanged += Component_PropertyChanged;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ComponentViewModel cvm in e.OldItems)
                {
                    Course.Grade.Subtract(cvm.Component.WeightedGrade);
                    cvm.Component.PropertyChanged -= Component_PropertyChanged;
                }
            }
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("WeightedGrade"))
            {
                Course.Grade.Reset();
                foreach (ComponentViewModel cvm in ComponentViewModels)
                    if(cvm.AssignmentViewModels.Count > 0)
                        Course.Grade.Add(cvm.Component.WeightedGrade);
            }
        }

        /// <summary>
        /// Shows a dialog box to enable the user to edit some info about the <see cref="Course"/>
        /// </summary>
        public async void EditCourseInfo()
        {
            //Backs up the data before its changed in case of need to revert back
            string name = Course.Name;
            double aLow = Course.ARangeLowEnd;
            double aHigh = Course.ARangeHighEnd;
            double bLow = Course.BRangeLowEnd;
            double bHigh = Course.BRangeHighEnd;
            double cLow = Course.CRangeLowEnd;
            double cHigh = Course.CRangeHighEnd;
            double nrLow = Course.NRRangeLowEnd;
            double nrHigh = Course.NRRangeHighEnd;

            CourseInfoDialogViewModel dialogViewModel = new CourseInfoDialogViewModel(Course);  //Create a new dialog
            var result = await dialogViewModel.GetDialogResult();                               //Get for the result
            if(result == ContentDialogResult.Primary)                                           //if the user clicks save
            {
                courseRepository.UpdateItem(Course.Id, Course);                                     //then update the item in the database
            }
            else if (result == ContentDialogResult.Secondary)                                   //otherwise revert back to old data
            {
                Course.Name = name;
                Course.ARangeLowEnd = aLow;
                Course.ARangeHighEnd = aHigh;
                Course.BRangeLowEnd = bLow;
                Course.BRangeHighEnd = bHigh;
                Course.CRangeLowEnd = cLow;
                Course.CRangeHighEnd = cHigh;
                Course.NRRangeLowEnd = nrLow;
                Course.NRRangeHighEnd = nrHigh;
            }
        }

        /// <summary>
        /// Adds a new <see cref="ComponentViewModel"/> that holds a <see cref="Component"/>
        /// </summary>
        public void AddNewComponent()
        {
            ComponentViewModel newComponentModel = new ComponentViewModel(Course.Id);   //Create a new componentviewmodel instance with the id of the course
            componentRepository.InsertItem(newComponentModel.Component);                //Insert the new item in the databse
            ComponentViewModels.Add(newComponentModel);                                 //Add the new item in the list of componentviewmodels
            SelectedComponentViewModel = newComponentModel;                             //set the new item as the selected one
            newComponentModel.IsInEditMode = true;                                      //set its edit mode to true so the user can edit it
            onPropertyChanged("CanShowComponentDetailPanel");                           //Notify the CanShowComponentDetailPanel of the changes
        }

        /// <summary>
        /// Deletes an <see cref="ComponentViewModel"/> and the <see cref="Component"/> that it holds
        /// </summary>
        /// <param name="componentViewModel">The ComponentViewerViewModel to be deleted</param>
        public async void DeleteComponent(ComponentViewModel componentViewModel)
        {
            ContentDialog deleteDialog = new ContentDialog();                                                                                                               //Create a new dialog
            deleteDialog.Title = "Are you Sure you want to delete this Component?";                                                                                         //add a title
            deleteDialog.PrimaryButtonText = "Yes";                                                                                                                         //Make the primary button Yes
            deleteDialog.SecondaryButtonText = "No";                                                                                                                        //Make the secondary button No
            var result = await deleteDialog.ShowAsync();                                                                                                                    //Get the result
            if (result == ContentDialogResult.Primary)                                                                                                                      // if the user clicks yes
            {
                if (ComponentViewModels.Count > 1 && selectedComponentViewModel == componentViewModel)                                                                          //if the componentviewmodel to be deleted is the selected one and there are more other componentviewmodels in the list
                    SelectedComponentViewModel = selectedComponentViewModel == ComponentViewModels.First() ? ComponentViewModels.ElementAt(1) : ComponentViewModels.First();        //if the selected one is the first one selected the second one otherwise select the first one
                componentRepository.DeleteItem(componentViewModel.Component.Id);                                                                                                //Delete from the database
                ComponentViewModels.Remove(componentViewModel);                                                                                                                 //remove from the list of componentviewmodels
                onPropertyChanged("CanShowComponentDetailPanel");                                                                                                               //Notify the canShowComponentDetailPanel of the changes
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentViewModel"></param>
        public void EditComponent(ComponentViewModel componentViewModel)
        {
            if(selectedComponentViewModel != componentViewModel)    //if the currently selected component is not the given component
                SelectedComponentViewModel = componentViewModel;    //then set the given one as the selected on
            componentViewModel.IsInEditMode = true;                 //set edit mode to true for that componentviewmodel
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentViewModel"></param>
        public void SaveComponentChanges(ComponentViewModel componentViewModel)
        {
            componentRepository.UpdateItem(componentViewModel.Component.Id, componentViewModel.Component);  //Update the item in the data base
            componentViewModel.IsInEditMode = false;                                                        //set editing mode to false
        }
        #endregion


    }
}
