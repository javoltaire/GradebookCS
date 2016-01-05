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
        /// an instance of the MainPageViewModel to have access to methods
        /// </summary>
        private MainPageViewModel mainPageViewModel;
        #endregion

        #region Properties

        #region Command Properties
        /// <summary>
        /// Command to add a new Course
        /// </summary>
        public RelayCommand AddNewCourseCommand { get; private set; }

        /// <summary>
        /// Command to show course details
        /// </summary>
        public RelayParameterCommand<CourseViewerViewModel> ViewCourseDetailsCommand { get; private set; }
        #endregion

        #region Other Properties
        /// <summary>
        /// Gets the list of courses
        /// </summary>
        public static ObservableCollection<CourseViewerViewModel> CourseViewModels { get; private set; }
        #endregion

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize a new instance of this class.
        /// </summary>
        public CourseListPageViewModel(MainPageViewModel mainPageViewModel)
        {
            this.mainPageViewModel = mainPageViewModel;
            CourseViewModels = new ObservableCollection<CourseViewerViewModel>();
            AddNewCourseCommand = new RelayCommand(() => AddNewCourse(), () => true);
            ViewCourseDetailsCommand = new RelayParameterCommand<CourseViewerViewModel>(ViewCourseDetails, () => true);

            PopulateCoursesList();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a new course (courseviewModel) to the list of course
        /// </summary>
        public async void AddNewCourse()
        {
            CourseViewerViewModel newCourseViewModel = new CourseViewerViewModel();
            ContentDialogResult result = await newCourseViewModel.EditCourseInfo();
            if (result == ContentDialogResult.Primary)
                CourseViewModels.Add(newCourseViewModel);
        }

        
        public void ViewCourseDetails(CourseViewerViewModel courseViewerViewModel)
        {
            mainPageViewModel.SelectedCourseViewerViewModel = courseViewerViewModel;
            mainPageViewModel.CurrentPageType = typeof(CourseDetailsPage);
        }
        #endregion

        #region To be Deleted
        private void PopulateCoursesList()
        {
            Course CS101 = new Course("CS 101");

            Component tests = new Component("Tests", 60);
            Component quizzes = new Component("Quizzes", 15);
            Component homework = new Component("Homework", 25);

            Assignment test1 = new Assignment("Test 1", 90, 100);
            Assignment test2 = new Assignment("Test 2", 94, 100);
            Assignment test3 = new Assignment("Test 3", 85, 100);

            Assignment quizz1 = new Assignment("Quizz 1", 90, 100);
            Assignment quizz2 = new Assignment("Quizz 2", 94, 100);
            Assignment quizz3 = new Assignment("Quizz 3", 85, 100);
            Assignment quizz4 = new Assignment("Quizz 4", 90, 100);
            Assignment quizz5 = new Assignment("Quizz 5", 94, 100);
            Assignment quizz6 = new Assignment("Quizz 6", 85, 100);

            Assignment homework1 = new Assignment("Homework 1", 90, 100);
            Assignment homework2 = new Assignment("Homework 2", 94, 100);
            Assignment homework3 = new Assignment("Homework 3", 85, 100);
            Assignment homework4 = new Assignment("Homework 4", 90, 100);

            tests.Assignments.Add(test1);
            tests.Assignments.Add(test2);
            tests.Assignments.Add(test3);

            quizzes.Assignments.Add(quizz1);
            quizzes.Assignments.Add(quizz2);
            quizzes.Assignments.Add(quizz3);
            quizzes.Assignments.Add(quizz4);
            quizzes.Assignments.Add(quizz5);
            quizzes.Assignments.Add(quizz6);

            homework.Assignments.Add(homework1);
            homework.Assignments.Add(homework2);
            homework.Assignments.Add(homework3);
            homework.Assignments.Add(homework4);

            CS101.Components.Add(tests);
            CS101.Components.Add(quizzes);
            CS101.Components.Add(homework);

            CourseViewerViewModel cvm1 = new CourseViewerViewModel(CS101);
            CourseViewModels.Add(cvm1);
        }
        #endregion
    }
}
