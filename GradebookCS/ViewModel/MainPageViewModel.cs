using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.DataBase;
using Windows.UI.Xaml.Controls;
using GradebookCS.View;
using System.Collections.ObjectModel;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;

namespace GradebookCS.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Properties
        /// <summary>
        /// Gets the page type being shown
        /// </summary>
        public Type CurrentPageType { get; set; }

        /// <summary>
        /// Gets the boolean indicating wheter the current page is already the settings page
        /// </summary>
        public bool CanShowSettingsPage { get { return CurrentPageType != typeof(SettingsPage); } }

        #region CommandProperties
        /// <summary>
        /// The command to View settings
        /// </summary>
        public ViewSettingsCommand ViewSettingsCommand { get; private set; }
        #endregion

        #region ViewModelsProperties
        /// <summary>
        /// Gets the COurseListPageViewModel
        /// </summary>
        public CourseListPageViewModel CourseListPageViewModel { get; private set; }
        #endregion

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the MainPageViewModel class
        /// </summary>
        public MainPageViewModel()
        {
            CurrentPageType = typeof(CourseListPage);
            ViewSettingsCommand = new ViewSettingsCommand(this);
            CourseListPageViewModel = new CourseListPageViewModel();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shows the Settings page
        /// </summary>
        public void ViewSettingsPage()
        {
            CurrentPageType = typeof(SettingsPage);
            onPropertyChanged("CurrentPageType");
        }
        #endregion









        public void ViewCourseDetail()
        {
            CurrentPageType = typeof(CourseDetailsPage);
            onPropertyChanged("CurrentPageType");
        }

        private void PopulateCoursesList()
        {
            Course MA101 = new Course("MA 101");
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

            //Courses.Add(MA101);
            //Courses.Add(CS101);
        }




    }
}
