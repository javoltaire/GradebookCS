using GradebookCS.Model;
using GradebookCS.View;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GradebookCS.Common;
using System.Collections.Specialized;
using GradebookCS.DataBase;

namespace GradebookCS.ViewModel
{
    public class CourseListViewModel : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// an instance of the MainPageViewModel to have access to methods
        /// </summary>
        private MainViewModel mainPageViewModel;

        /// <summary>
        /// An instance of the database table containing all the course
        /// </summary>
        private CourseTable courseRepository = CourseTable.Instance;
        #endregion

        #region Properties
        /// <summary>
        /// Boolean indicating whether the list containing all the courses (courseViewmodels) is empty
        /// </summary>
        public bool CanShowCourseListGridview { get { return CourseViewModels.Count > 0; } }

        /// <summary>
        /// Gets the list of courses
        /// </summary>
        public ObservableCollection<CourseViewModel> CourseViewModels { get; private set; } = new ObservableCollection<CourseViewModel>();
        #endregion

        #region Command Properties
        /// <summary>
        /// Command to add a new Course
        /// </summary>
        public RelayCommand AddNewCourseCommand { get; private set; }

        /// <summary>
        /// Command to show course details
        /// </summary>
        public RelayParameterCommand<CourseViewModel> ViewCourseDetailsCommand { get; private set; }

        /// <summary>
        /// Command to delele this course
        /// </summary>
        public RelayParameterCommand<CourseViewModel> DeleteCourseCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize a new instance of this class.
        /// </summary>
        public CourseListViewModel(MainViewModel mainPageViewModel)
        {
            this.mainPageViewModel = mainPageViewModel;
            AddNewCourseCommand = new RelayCommand(() => AddNewCourse(), () => true);
            ViewCourseDetailsCommand = new RelayParameterCommand<CourseViewModel>(ViewCourseDetails, () => true);
            DeleteCourseCommand = new RelayParameterCommand<CourseViewModel>(DeleteCourse, () => true);
            LoadCourses();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds all items retrieved from the database to this list
        /// </summary>
        private void LoadCourses()
        {
            var coursesData = courseRepository.GetAllItems();               //Get all the objects from database
            foreach(var course in coursesData)                              //Loop through all the items in the list
            {
                CourseViewModel viewModel = new CourseViewModel(course);        //Create a course viewmodel for the current course item
                CourseViewModels.Add(viewModel);                                //Add the view model to the list of courseviewmodels
            }
        }

        /// <summary>
        /// Navigates to the details page to show the course components and assignments and such
        /// </summary>
        /// <param name="courseViewerViewModel">The course to view details of</param>
        public void ViewCourseDetails(CourseViewModel courseViewerViewModel)
        {
            mainPageViewModel.SelectedCourseViewModel = courseViewerViewModel;  //Set the selected course to be viewed to the given one
            mainPageViewModel.CurrentPageType = typeof(CourseDetailsPage);      //Navigate to the details page
        }

        /// <summary>
        /// Adds a new course (courseviewModel) to the list of course
        /// </summary>
        public async void AddNewCourse()
        {
            CourseViewModel newCourseViewModel = new CourseViewModel();                                         //Create a new CourseViewModel
            CourseInfoDialogViewModel infoDialog = new CourseInfoDialogViewModel(newCourseViewModel.Course);    //Create a new Dialog so the user can edit its properties
            var result = await infoDialog.GetDialogResult();                                                    //Get the result
            if (result == ContentDialogResult.Primary)                                                          //If the user clicks save
            {
                courseRepository.InsertItem(newCourseViewModel.Course);                                             //Insert the item in the database
                CourseViewModels.Add(newCourseViewModel);                                                           //Add the item in the list of viewmodels
                onPropertyChanged("CanShowCourseListGridview");                                                     //Notify the CanShowCourseListGridView Property of the changes
            }

        }

        /// <summary>
        /// Deletes this courseviewmodel from its parent list
        /// </summary>
        public async void DeleteCourse(CourseViewModel courseViewModel)
        {
            ContentDialog deleteDialog = new ContentDialog();                       //Create a new dialog
            deleteDialog.Title = "Are you Sure you want to delete this course?";    //set the title of it
            deleteDialog.PrimaryButtonText = "Yes";                                 //make the primary button Yes
            deleteDialog.SecondaryButtonText = "No";                                //make the primary button No
            var result = await deleteDialog.ShowAsync();                            //Get the result
            if (result == ContentDialogResult.Primary)                              //if the user clicks yes
            {
                courseRepository.DeleteItem(courseViewModel.Course.Id);                 //Delete the item from the database
                CourseViewModels.Remove(courseViewModel);                               //Remove it from the list
                onPropertyChanged("CanShowCourseListGridview");                          //Notify the CanShowCourseListGridView property of the changes
            }

        }
        #endregion
    }
}
