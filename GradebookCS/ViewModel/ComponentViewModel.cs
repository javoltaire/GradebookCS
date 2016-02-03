using GradebookCS.Common;
using GradebookCS.DataBase;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel
{
    public class ComponentViewModel : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// States whether or not the component view should be in edit mode
        /// </summary>
        private bool isInEditMode = false;

        /// <summary>
        /// The current Assignment being edited
        /// </summary>
        private AssignmentViewModel assignmentInEditMode = null;

        /// <summary>
        /// An instance of the assignment table repository
        /// </summary>
        private AssignmentTable assignmentRepository = AssignmentTable.Instance;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the boolean indicating whether this Component is being edited
        /// </summary>
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                if (value != isInEditMode)
                {
                    isInEditMode = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The component for this componentviewmodel
        /// </summary>
        public Component Component { get; private set; } = new Component();
        
        /// <summary>
        /// A list containing all the assignments (assignmentviewmodels) for this particular component
        /// </summary>
        public ObservableCollection<AssignmentViewModel> AssignmentViewModels { get; private set; } = new ObservableCollection<AssignmentViewModel>();

        /// <summary>
        /// Command to add a new Assignment
        /// </summary>
        public RelayCommand AddNewAssignmentCommand { get; private set; }

        /// <summary>
        /// Command to Remove an Assingment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> DeleteAssignmentCommand { get; private set; }

        /// <summary>
        /// Command to Show editing controls like textboxes for an assignment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> EditAssignmentCommand { get; private set; }

        /// <summary>
        /// Command to hide editing controls like textboxes for an assignment
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> SaveAssignmentChangesCommand { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to initialize an instance of the class
        /// </summary>
        /// <param name="component">The component for this componentviewmodel</param>
        public ComponentViewModel(Component component)
        {
            this.Component = component;
            AddNewAssignmentCommand = new RelayCommand(AddNewAssignment, () => true);
            DeleteAssignmentCommand = new RelayParameterCommand<AssignmentViewModel>(DeleteAssignment, () => true);
            EditAssignmentCommand = new RelayParameterCommand<AssignmentViewModel>(EditAssignment, () => true);
            SaveAssignmentChangesCommand = new RelayParameterCommand<AssignmentViewModel>(SaveAssignmentChanges, () => true);
            LoadAssigmentsData();
        }

        /// <summary>
        /// Constructor to initialize an instance of this class and assigning the parent course id for the new component
        /// </summary>
        /// <param name="courseId">The parent course ID</param>
        public ComponentViewModel(string courseId)
        {
            this.Component.CourseId = courseId;
            AddNewAssignmentCommand = new RelayCommand(AddNewAssignment, () => true);
            DeleteAssignmentCommand = new RelayParameterCommand<AssignmentViewModel>(DeleteAssignment, () => true);
            EditAssignmentCommand = new RelayParameterCommand<AssignmentViewModel>(EditAssignment, () => true);
            SaveAssignmentChangesCommand = new RelayParameterCommand<AssignmentViewModel>(SaveAssignmentChanges, () => true);
            LoadAssigmentsData();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load all the assignments for this component
        /// </summary>
        private void LoadAssigmentsData()
        {
            var items = assignmentRepository.GetAllItemsForId(this.Component.Id);   //get all the items for this component
            foreach (Assignment a in items)                                          //Loop through all the items
            {
                AssignmentViewModel avm = new AssignmentViewModel(a);                   //create a viewmodel for that assignment instance of assignment
                Component.TotalGrade.Add(avm.Assignment.Grade);
                avm.Assignment.Grade.PropertyChanged += AssignmentGrade_PropertyChanged;
                AssignmentViewModels.Add(avm);                                          //Add the viewmodel to the list of assignmentviewmodels
            }
            AssignmentViewModels.CollectionChanged += AssignmentViewModels_CollectionChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignmentViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (AssignmentViewModel newItem in e.NewItems)
                {
                    Component.TotalGrade.Add(newItem.Assignment.Grade);
                    newItem.Assignment.Grade.PropertyChanged += AssignmentGrade_PropertyChanged;
                }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (AssignmentViewModel oldIem in e.OldItems)
                {
                    Component.TotalGrade.Subtract(oldIem.Assignment.Grade);
                    oldIem.Assignment.Grade.PropertyChanged -= AssignmentGrade_PropertyChanged;
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignmentGrade_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Score") || e.PropertyName.Equals("MaximumScore"))
            {
                Component.TotalGrade.Reset();
                foreach (AssignmentViewModel avm in AssignmentViewModels)
                    Component.TotalGrade.Add(avm.Assignment.Grade);
            }
        }

        /// <summary>
        /// Creates a new <see cref="AssignmentViewModel"/>  and adds it to the list/>
        /// </summary>
        public void AddNewAssignment()
        {
            AssignmentViewModel newAssignmentViewModel = new AssignmentViewModel(Component.Id); //Create a new AssignmentViewmodel and pass it the id of this component
            AssignmentViewModels.Add(newAssignmentViewModel);                                   //Add this new assignmentviewmodel to the list
            assignmentRepository.InsertItem(newAssignmentViewModel.Assignment);                 //Insert that assignment in the database
            EditAssignment(newAssignmentViewModel);                                             //go in to edit mode
        }

        /// <summary>
        /// Deletes an <see cref="AssignmentViewModel"/> and the <see cref="Assignment"/> that it holds
        /// </summary>
        /// <param name="assignmentToBeDeleted">The AssignmentViewModel to be deleted</param>
        public async void DeleteAssignment(AssignmentViewModel assignmentToBeDeleted)
        {
            ContentDialog deleteDialog = new ContentDialog();                               //Create a new dialog
            deleteDialog.Title = "Are you Sure you want to delete this Assignment?";        //Add a title to the dialog
            deleteDialog.PrimaryButtonText = "Yes";                                         //Make primary button Yes
            deleteDialog.SecondaryButtonText = "No";                                        //Make secondary button no
            var result = await deleteDialog.ShowAsync();                                    //get the result of the dialog
            if (result == ContentDialogResult.Primary)                                      //if the user clicks yes (Primary button)
            {
                assignmentRepository.DeleteItem(assignmentToBeDeleted.Assignment.Id);           //Delete from database
                AssignmentViewModels.Remove(assignmentToBeDeleted);                             //Delete from list of viewModels
            }
        }

        /// <summary>
        /// Puts the given assignmentViewModel in edit mode
        /// </summary>
        /// <param name="assignmentViewModel">The assingmentviewmodel that needs to be edited</param>
        public void EditAssignment(AssignmentViewModel assignmentViewModel)
        {
            if (assignmentInEditMode != null)                               //check if assignmentInEditMode is null
                SaveAssignmentChanges(assignmentInEditMode);                    //if not then save the changes
            assignmentInEditMode = assignmentViewModel;                     //Set the given assignmentviewmodel as the one being edited
            assignmentViewModel.IsInEditMode = true;                        //Set edit mode of that assignmentviewmodel to true
        }

        /// <summary>
        /// Saves and puts the given assignmentViewModel in normal mode
        /// </summary>
        /// <param name="assignmentViewModel">Assignment to save</param>
        public void SaveAssignmentChanges(AssignmentViewModel assignmentViewModel)
        {
            assignmentRepository.UpdateItem(assignmentViewModel.Assignment.Id, assignmentViewModel.Assignment); //Update the assignment in the database
            assignmentViewModel.IsInEditMode = false;                                                           //Set edit mode of that assignment to false
            assignmentInEditMode = null;                                                                        //set the assignmentInEditMode to null
        }

        
        #endregion


    }
}
