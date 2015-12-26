using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.Model;

namespace GradebookCS.DataBase
{
    /// <summary>
    /// This class holds all the courses
    /// </summary
    /// <remarks>
    /// This class follow singleton pattern to make sure there is only one instance of it to hold all the courses
    /// </remarks>
    public class CoursesManagerSingleton
    {
        #region Attributes
        private static CoursesManagerSingleton instance = new CoursesManagerSingleton();
        #endregion

        #region Properties
        /// <summary>
        /// Gets an instance of this class
        /// </summary>
        /// <remarks>This property provide static access to the one instance of this class</remarks>
        /// <value>The instance created</value>
        public static CoursesManagerSingleton INSTANCE
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets or Sets the List of Courses
        /// </summary>
        /// <remarks>This list is public, however it can only be set in this class</remarks>
        /// <value>The List of <see cref="Course"/>s</value>
        public ObservableCollection<Course> Courses { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to Instantiate this class and its attributes and Properties
        /// </summary>
        /// <remarks>This Constructor is private to make sure it can't be instantiated from outside this class</remarks>
        private CoursesManagerSingleton()
        {
            this.Courses = new ObservableCollection<Course>();
            PopulateCoursesList();
        }
        #endregion

        private void PopulateCoursesList()
        {
            Course CS101 = new Course("CS 101");

            //Component tests = new Component("Tests", 60);
            //Component quizzes = new Component("Quizzes", 15);
            //Component homework = new Component("Homework", 25);

            //Assignment test1 = new Assignment("Test 1", 90, 100);
            //Assignment test2 = new Assignment("Test 2", 94, 100);
            //Assignment test3 = new Assignment("Test 3", 85, 100);

            //Assignment quizz1 = new Assignment("Quizz 1", 90, 100);
            //Assignment quizz2 = new Assignment("Quizz 2", 94, 100);
            //Assignment quizz3 = new Assignment("Quizz 3", 85, 100);
            //Assignment quizz4 = new Assignment("Quizz 4", 90, 100);
            //Assignment quizz5 = new Assignment("Quizz 5", 94, 100);
            //Assignment quizz6 = new Assignment("Quizz 6", 85, 100);

            //Assignment homework1 = new Assignment("Homework 1", 90, 100);
            //Assignment homework2 = new Assignment("Homework 2", 94, 100);
            //Assignment homework3 = new Assignment("Homework 3", 85, 100);
            //Assignment homework4 = new Assignment("Homework 4", 90, 100);

            //tests.Assignments.Add(test1);
            //tests.Assignments.Add(test2);
            //tests.Assignments.Add(test3);

            //quizzes.Assignments.Add(quizz1);
            //quizzes.Assignments.Add(quizz2);
            //quizzes.Assignments.Add(quizz3);
            //quizzes.Assignments.Add(quizz4);
            //quizzes.Assignments.Add(quizz5);
            //quizzes.Assignments.Add(quizz6);

            //homework.Assignments.Add(homework1);
            //homework.Assignments.Add(homework2);
            //homework.Assignments.Add(homework3);
            //homework.Assignments.Add(homework4);

            //CS101.Components.Add(tests);
            //CS101.Components.Add(quizzes);
            //CS101.Components.Add(homework);

            Courses.Add(CS101);
        }
    }
}
