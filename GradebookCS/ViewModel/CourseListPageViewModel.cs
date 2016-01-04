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

namespace GradebookCS.ViewModel
{
    public class CourseListPageViewModel
    {
        #region Attributes
        /// <summary>
        /// The currently selected CourseViewModel
        /// </summary>
        private CourseViewModel selectedCourseViewModel;

        /// <summary>
        /// an instance of the MainPageViewModel to have access to methods
        /// </summary>
        private MainPageViewModel mainPageViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list of courses
        /// </summary>
        public static ObservableCollection<CourseViewModel> CourseViewModels { get; private set; }
        public CourseViewModel SelectedCourseViewModel
        {
            get { return selectedCourseViewModel; }
            set
            {
                selectedCourseViewModel = value;
                mainPageViewModel.CurrentPageType = typeof(CourseDetailsPage);
                selectedCourseViewModel = null;
            }
        }
        /// <summary>
        /// Command to add a new Course
        /// </summary>
        public AddNewCourseCommand AddNewCourseCommand { get; private set; }

        public ViewCourseDetailsCommand ViewCourseDetailsCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize a new instance of this class.
        /// </summary>
        public CourseListPageViewModel(MainPageViewModel mainPageViewModel)
        {
            this.mainPageViewModel = mainPageViewModel;
            CourseViewModels = new ObservableCollection<CourseViewModel>();
            AddNewCourseCommand = new AddNewCourseCommand(this);
            ViewCourseDetailsCommand = new ViewCourseDetailsCommand(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a new course (courseviewModel) to the list of course
        /// </summary>
        public async void AddNewCourse()
        {
            CourseViewModel newCourseViewModel = new CourseViewModel();
            ContentDialogResult result = await newCourseViewModel.EditCourseInfo();
            if (result == ContentDialogResult.Primary)
                CourseViewModels.Add(newCourseViewModel);
        }

        public void ViewCourseDetails()
        {
            mainPageViewModel.CurrentPageType = typeof(CourseDetailsPage);
            //mainPageViewModel.SelectedCourse = 
        }
        #endregion
    }
}
