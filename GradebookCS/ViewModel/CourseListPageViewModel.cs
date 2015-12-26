using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.ViewModel
{
    public class CourseListPageViewModel
    {
        #region Attributes
        private MainPageViewModel mainPageViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list of courses
        /// </summary>
        public ObservableCollection<Course> Courses { get; private set; }

        public AddNewCourseCommand AddNewCourseCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize a new instance of this class.
        /// </summary>
        /// <param name="mainPageViewModel"></param>
        public CourseListPageViewModel(MainPageViewModel mainPageViewModel)
        {
            this.mainPageViewModel = mainPageViewModel;
            Courses = new ObservableCollection<Course>();
            AddNewCourseCommand = new AddNewCourseCommand(this);
        }
        #endregion

        #region Methods
        public void AddNewCourse()
        {
            Course course = new Course();
            Courses.Add(course);
        }
        #endregion
    }
}
