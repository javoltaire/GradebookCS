using GradebookCS.Common;
using GradebookCS.DataBase;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using GradebookCS.ViewModel.UserControlsViewModels;
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
        public RelayParameterCommand<ComponentViewModel> ShowComponentEditingControlsCommand { get; private set; }

        /// <summary>
        /// Command to hide editing controls like textboxes for a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> HideComponentEditingControlsCommand { get; private set; }
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
                        HideComponentEditingControls(selectedComponentViewModel);
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
                ComponentViewModels.Add(cvm);                                   //Add the new componentviewmodel to the list.
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

            CourseInfoDialogViewModel dialogViewModel = new CourseInfoDialogViewModel(Course);
            var result = await dialogViewModel.GetDialogResult();
            if(result == ContentDialogResult.Primary)
            {
                courseRepository.UpdateItem(Course.Id, Course);
            }
            else if (result == ContentDialogResult.Secondary)
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
            ComponentViewModel newComponentModel = new ComponentViewModel(Course.Id);
            newComponentModel.IsInEditMode = true;
            ComponentViewModels.Add(newComponentModel);
            componentRepository.InsertItem(newComponentModel.Component);
            SelectedComponentViewModel = newComponentModel;
            onPropertyChanged("CanShowComponentDetailPanel");
        }

        /// <summary>
        /// Deletes an <see cref="ComponentViewModel"/> and the <see cref="Component"/> that it holds
        /// </summary>
        /// <param name="componentViewModel">The ComponentViewerViewModel to be deleted</param>
        public async void DeleteComponent(ComponentViewModel componentViewModel)
        {
            ContentDialog deleteDialog = new ContentDialog();
            deleteDialog.Title = "Are you Sure you want to delete this Component?";
            deleteDialog.PrimaryButtonText = "Yes";
            deleteDialog.SecondaryButtonText = "No";
            var result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (ComponentViewModels.Count > 1 && selectedComponentViewModel == componentViewModel)
                    SelectedComponentViewModel = selectedComponentViewModel == ComponentViewModels.First() ? ComponentViewModels.ElementAt(1) : ComponentViewModels.First();
                componentRepository.DeleteItem(componentViewModel.Component.Id);
                ComponentViewModels.Remove(componentViewModel);
                onPropertyChanged("CanShowComponentDetailPanel");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentViewModel"></param>
        public void ShowComponentEditingControls(ComponentViewModel componentViewModel)
        {
            if(selectedComponentViewModel != componentViewModel)
                SelectedComponentViewModel = componentViewModel;
            componentViewModel.IsInEditMode = true;
            AddNewComponentCommand.OnCanExecuteChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentViewModel"></param>
        public void HideComponentEditingControls(ComponentViewModel componentViewModel)
        {
            componentRepository.UpdateItem(componentViewModel.Component.Id, componentViewModel.Component);
            componentViewModel.IsInEditMode = false;
            AddNewComponentCommand.OnCanExecuteChanged();
        }

        #endregion

        #region TO be refactored

        #region Properties
        #region Command Properties




        /// <summary>
        /// Command to add a new Assignment
        /// </summary>
        public RelayCommand AddNewAssignmentCommand { get; private set; }

        /// <summary>
        /// Command to Remove an Assingment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> RemoveAssignment { get; private set; }

        

        

        /// <summary>
        /// Command to Show editing controls like textboxes for an assignment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> ShowAssignmentEditingConstolsCommand { get; private set; }

        /// <summary>
        /// Command to hide editing controls like textboxes for an assignment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> HideAssignmentEditingConstolsCommand { get; private set; }
        #endregion

        #region Other Properties
        

        ///// <summary>
        ///// The selected compomemtViewModel
        ///// </summary>
        //public ComponentViewModel SelectedComponentViewModel
        //{
        //    get { return selectedComponentViewModel; }
        //    set
        //    {
        //        if (value != selectedComponentViewModel)
        //        {
        //            if (selectedComponentViewModel != null && selectedComponentViewModel.IsInEditMode)
        //            {
        //                selectedComponentViewModel.IsInEditMode = false;
        //                selectedComponentViewModel.Component.PropertyChanged -= Component_PropertyChanged;
        //            }
        //            selectedComponentViewModel = value;
        //            //selectedComponentViewModel.Component.PropertyChanged += Component_PropertyChanged;
        //            onPropertyChanged();
        //            onPropertyChanged("CourseAndComponentName");
        //        }
        //    }
        //}

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                onPropertyChanged("CourseAndComponentName");
            }
        }

        

        
        #endregion
        #endregion

        #region Constructor
        ///// <summary>
        ///// Initializes an instance of this class
        ///// </summary>
        //public CourseViewModel()
        //{
        //    EditCourseInfoCommand = new RelayCommand(EditCourseInfo, () => true);
        //    AddNewComponentCommand = new RelayCommand(AddNewComponent, () => true);
        //    AddNewAssignmentCommand = new RelayCommand(AddNewAssignment, () => true);
        //    RemoveAssignment = new RelayParameterCommand<AssignmentViewModel>(DeleteAssignment, () => true);
        //    RemoveComponent = new RelayParameterCommand<ComponentViewModel>(DeleteComponent, () => true);
        //    ShowComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(ShowComponentEditingControls, () => true);
        //    HideComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(HideComponentEditingControls, () => true);
        //    ShowAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(ShowAssignmentEditingConstols, () => true);
        //    HideAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(HideAssignmentEditingConstols, () => true);
        //    ComponentViewModels.CollectionChanged += ComponentViewerViewModels_CollectionChanged;
        //    //PopulateModels();
        //}
        #endregion

        #region Methods
        

        ///// <summary>
        ///// Adds a new <see cref="ComponentViewModel"/> that holds a <see cref="Component"/>
        ///// </summary>
        //public void AddNewComponent()
        //{
        //    ComponentViewModel newComponentModel = new ComponentViewModel();
        //    ComponentViewModels.Add(newComponentModel);
        //    Course.Components.Add(newComponentModel.Component);
        //    SelectedComponentViewModel = newComponentModel;
        //    newComponentModel.IsInEditMode = true;
        //}

        /// <summary>
        /// Adds a new <see cref="AssignmentViewModel"/> that holds a <see cref="Assignment"/>
        /// </summary>
        public void AddNewAssignment()
        {
            AssignmentViewModel newAssignmentViewModel = new AssignmentViewModel();
            selectedComponentViewModel.AssignmentViewerViewModels.Add(newAssignmentViewModel);
            selectedComponentViewModel.Component.Assignments.Add(newAssignmentViewModel.Assignment);
            ShowAssignmentEditingConstols(newAssignmentViewModel);
        }

        /// <summary>
        /// Deletes an <see cref="AssignmentViewModel"/> and the <see cref="Assignment"/> that it holds
        /// </summary>
        /// <param name="assignmentToBeDeleted">The AssignmentViewerViewModel to be deleted</param>
        public async void DeleteAssignment(AssignmentViewModel assignmentToBeDeleted)
        {
            ContentDialog deleteDialog = new ContentDialog();
            deleteDialog.Title = "Are you Sure you want to delete this Assignment?";
            deleteDialog.PrimaryButtonText = "Yes";
            deleteDialog.SecondaryButtonText = "No";
            var result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                selectedComponentViewModel.Component.Assignments.Remove(selectedComponentViewModel.AssignmentViewerViewModels.ElementAt(selectedComponentViewModel.AssignmentViewerViewModels.IndexOf(assignmentToBeDeleted)).Assignment);
                selectedComponentViewModel.AssignmentViewerViewModels.Remove(assignmentToBeDeleted);
            }

        }

        ///// <summary>
        ///// Deletes an <see cref="ComponentViewModel"/> and the <see cref="Component"/> that it holds
        ///// </summary>
        ///// <param name="componentViewerViewModel">The ComponentViewerViewModel to be deleted</param>
        //public async void DeleteComponent(ComponentViewModel componentViewerViewModel)
        //{
        //    ContentDialog deleteDialog = new ContentDialog();
        //    deleteDialog.Title = "Are you Sure you want to delete this Component?";
        //    deleteDialog.PrimaryButtonText = "Yes";
        //    deleteDialog.SecondaryButtonText = "No";
        //    var result = await deleteDialog.ShowAsync();
        //    if (result == ContentDialogResult.Primary)
        //    {
        //        if (ComponentViewModels.Count > 1 && selectedComponentViewModel == componentViewerViewModel)
        //            SelectedComponentViewModel = selectedComponentViewModel == ComponentViewModels.First() ? ComponentViewModels.ElementAt(1) : ComponentViewModels.First();
        //        Course.Components.Remove(componentViewerViewModel.Component);
        //        ComponentViewModels.Remove(componentViewerViewModel);

        //    }
        //}

        

        public void ShowAssignmentEditingConstols(AssignmentViewModel assignmentViewModel)
        {
            if (currentEditingAssignment != null)
                currentEditingAssignment.IsInEditMode = false;
            CurrentEditingAssignment = assignmentViewModel;
            assignmentViewModel.IsInEditMode = true;
        }

        public void HideAssignmentEditingConstols(AssignmentViewModel assignmentViewModel)
        {
            currentEditingAssignment = null;
            assignmentViewModel.IsInEditMode = false;
        }
        #endregion

        #region to be deleted
        private AssignmentViewModel currentEditingAssignment;
        public AssignmentViewModel CurrentEditingAssignment
        {
            get { return currentEditingAssignment; }
            set
            {
                if (value != currentEditingAssignment)
                {
                    currentEditingAssignment = value;
                }
            }
        }

        public CourseViewModel(Course course)
        {
            this.Course = course;
            EditCourseInfoCommand = new RelayCommand(EditCourseInfo, () => true);
            AddNewComponentCommand = new RelayCommand(AddNewComponent, () => true);
            RemoveComponentCommand = new RelayParameterCommand<ComponentViewModel>(DeleteComponent, () => true);
            AddNewAssignmentCommand = new RelayCommand(AddNewAssignment, () => true);
            ShowComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(ShowComponentEditingControls, () => true);
            HideComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(HideComponentEditingControls, () => true);
            ShowAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(ShowAssignmentEditingConstols, () => true);
            HideAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(HideAssignmentEditingConstols, () => true);
            LoadComponentsData();
            //PopulateModels();
        }

        private void PopulateModels()
        {
            Course.Components.Add(new Component());
            Course.Components.Add(new Component());
            Course.Components.Add(new Component());
        }


        #endregion
        #endregion


    }
}
