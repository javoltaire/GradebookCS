using GradebookCS.Model;
using GradebookCS.View;
using GradebookCS.ViewModel.Commands;
using GradebookCS.ViewModel.UserControlsViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.Common.Enums;
using Windows.UI.Xaml.Controls;
using GradebookCS.Common;
using System.Collections.Specialized;
using GradebookCS.DataBase;

namespace GradebookCS.ViewModel
{
    public class CourseListPageViewModel : BaseINPC
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
        public CourseListPageViewModel(MainViewModel mainPageViewModel)
        {
            this.mainPageViewModel = mainPageViewModel;
            AddNewCourseCommand = new RelayCommand(() => AddNewCourse(), () => true);
            ViewCourseDetailsCommand = new RelayParameterCommand<CourseViewModel>(ViewCourseDetails, () => true);
            DeleteCourseCommand = new RelayParameterCommand<CourseViewModel>(Delete, () => true);
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
            mainPageViewModel.SelectedCourseViewerViewModel = courseViewerViewModel;
            mainPageViewModel.CurrentPageType = typeof(CourseDetailsPage);
        }

        /// <summary>
        /// Adds a new course (courseviewModel) to the list of course
        /// </summary>
        public async void AddNewCourse()
        {
            CourseViewModel newCourseViewModel = new CourseViewModel();
            ContentDialogResult result = await newCourseViewModel.EditCourseInfo();
            if (result == ContentDialogResult.Primary)
            {
                CourseViewModels.Add(newCourseViewModel);
                courseRepository.InsertItem(newCourseViewModel.Course);
                onPropertyChanged("CanShowCourseListGridview");
            }

        }

        /// <summary>
        /// Deletes this courseviewmodel from its parent list
        /// </summary>
        public async void Delete(CourseViewModel courseViewModel)
        {
            ContentDialog deleteDialog = new ContentDialog();
            deleteDialog.Title = "Are you Sure you want to delete this course?";
            deleteDialog.PrimaryButtonText = "Yes";
            deleteDialog.SecondaryButtonText = "No";
            var result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                CourseViewModels.Remove(courseViewModel);
                courseRepository.DeleteItem(courseViewModel.Course.Id);
                onPropertyChanged("CanShowCourseListGridview");
            }

        }
        #endregion
    }
}
