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
        private CourseTable courseRepository = CourseTable.Instance;
        #endregion

        #region Command Properties
        /// <summary>
        /// Command to edit the course info like name and letter score ranges
        /// </summary>
        public RelayCommand EditCourseInfoCommand { get; private set; }
        #endregion

        #region Methods
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
        #endregion

        #region TO be refactored
        #region Attributes
        /// <summary>
        /// Store the selected ComponentViewerViewModel
        /// </summary>
        private ComponentViewModel selectedComponentViewModel;
        #endregion

        #region Properties
        #region Command Properties
        

        /// <summary>
        /// Commmand to add a new Component
        /// </summary>
        public RelayCommand AddNewComponentCommand { get; private set; }

        /// <summary>
        /// Command to add a new Assignment
        /// </summary>
        public RelayCommand AddNewAssignmentCommand { get; private set; }

        /// <summary>
        /// Command to Remove an Assingment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> RemoveAssignment { get; private set; }

        /// <summary>
        /// Command to Remove a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> RemoveComponent { get; private set; }

        /// <summary>
        /// Command to Show editing controls like textboxes for a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> ShowComponentEditingControlsCommand { get; private set; }

        /// <summary>
        /// Command to hide editing controls like textboxes for a component
        /// </summary>
        public RelayParameterCommand<ComponentViewModel> HideComponentEditingControlsCommand { get; private set; }

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
        /// <summary>
        /// The course
        /// </summary>
        public Course Course { get; set; } = new Course();

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
                        selectedComponentViewModel.IsInEditMode = false;
                        selectedComponentViewModel.Component.PropertyChanged -= Component_PropertyChanged;
                    }
                    selectedComponentViewModel = value;
                    //selectedComponentViewModel.Component.PropertyChanged += Component_PropertyChanged;
                    onPropertyChanged();
                    onPropertyChanged("CourseAndComponentName");
                }
            }
        }

        private void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                onPropertyChanged("CourseAndComponentName");
            }
        }

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
        /// 
        /// </summary>
        public ObservableCollection<ComponentViewModel> ComponentViewerViewModels { get; private set; } = new ObservableCollection<ComponentViewModel>();

        public bool CanShowComponentDetailPanel { get { return ComponentViewerViewModels.Count > 0; } }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes an instance of this class
        /// </summary>
        public CourseViewModel()
        {
            EditCourseInfoCommand = new RelayCommand(EditCourseInfo, () => true);
            AddNewComponentCommand = new RelayCommand(AddNewComponent, () => true);
            AddNewAssignmentCommand = new RelayCommand(AddNewAssignment, () => true);
            RemoveAssignment = new RelayParameterCommand<AssignmentViewModel>(DeleteAssignment, () => true);
            RemoveComponent = new RelayParameterCommand<ComponentViewModel>(DeleteComponent, () => true);
            ShowComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(ShowComponentEditingControls, () => true);
            HideComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(HideComponentEditingControls, () => true);
            ShowAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(ShowAssignmentEditingConstols, () => true);
            HideAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(HideAssignmentEditingConstols, () => true);
            ComponentViewerViewModels.CollectionChanged += ComponentViewerViewModels_CollectionChanged;
            //PopulateModels();
        }

        private void ComponentViewerViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
                onPropertyChanged("CanShowComponentDetailPanel");
        }
        #endregion

        #region Methods
        

        /// <summary>
        /// Adds a new <see cref="ComponentViewModel"/> that holds a <see cref="Component"/>
        /// </summary>
        public void AddNewComponent()
        {
            ComponentViewModel newComponentModel = new ComponentViewModel();
            ComponentViewerViewModels.Add(newComponentModel);
            Course.Components.Add(newComponentModel.Component);
            SelectedComponentViewModel = newComponentModel;
            newComponentModel.IsInEditMode = true;
        }

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

        /// <summary>
        /// Deletes an <see cref="ComponentViewModel"/> and the <see cref="Component"/> that it holds
        /// </summary>
        /// <param name="componentViewerViewModel">The ComponentViewerViewModel to be deleted</param>
        public async void DeleteComponent(ComponentViewModel componentViewerViewModel)
        {
            ContentDialog deleteDialog = new ContentDialog();
            deleteDialog.Title = "Are you Sure you want to delete this Component?";
            deleteDialog.PrimaryButtonText = "Yes";
            deleteDialog.SecondaryButtonText = "No";
            var result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (ComponentViewerViewModels.Count > 1 && selectedComponentViewModel == componentViewerViewModel)
                    SelectedComponentViewModel = selectedComponentViewModel == ComponentViewerViewModels.First() ? ComponentViewerViewModels.ElementAt(1) : ComponentViewerViewModels.First();
                Course.Components.Remove(componentViewerViewModel.Component);
                ComponentViewerViewModels.Remove(componentViewerViewModel);

            }
        }

        public void ShowComponentEditingControls(ComponentViewModel componentViewModel)
        {
            SelectedComponentViewModel = componentViewModel;
            componentViewModel.IsInEditMode = true;
            AddNewComponentCommand.OnCanExecuteChanged();
        }

        public void HideComponentEditingControls(ComponentViewModel componentViewModel)
        {
            componentViewModel.IsInEditMode = false;
            AddNewComponentCommand.OnCanExecuteChanged();
        }

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
            AddNewAssignmentCommand = new RelayCommand(AddNewAssignment, () => true);
            ShowComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(ShowComponentEditingControls, () => true);
            HideComponentEditingControlsCommand = new RelayParameterCommand<ComponentViewModel>(HideComponentEditingControls, () => true);
            ShowAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(ShowAssignmentEditingConstols, () => true);
            HideAssignmentEditingConstolsCommand = new RelayParameterCommand<AssignmentViewModel>(HideAssignmentEditingConstols, () => true);
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
