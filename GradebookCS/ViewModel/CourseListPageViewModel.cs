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
using GradebookCS.Enums;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel
{
    public class CourseListPageViewModel
    {
        #region Properties
        /// <summary>
        /// Gets the list of courses
        /// </summary>
        public ObservableCollection<CourseViewModel> CourseViewModels { get; private set; }

        #region CommandProperties
        /// <summary>
        /// Command to add a new Course
        /// </summary>
        public AddNewCourseCommand AddNewCourseCommand { get; private set; }
        #endregion
        

        


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize a new instance of this class.
        /// </summary>
        public CourseListPageViewModel()
        {
            CourseViewModels = new ObservableCollection<CourseViewModel>();
            AddNewCourseCommand = new AddNewCourseCommand(this);
        }
        #endregion

        #region Methods
        public async void AddNewCourse()
        {
            CourseViewModel newCourseViewModel = new CourseViewModel();
            CourseInfoDialogViewModel courseInfoDialogViewModel = new CourseInfoDialogViewModel(newCourseViewModel.Course);
            ContentDialogResult result = await courseInfoDialogViewModel.DialogResult();
            if (result == ContentDialogResult.Primary)
                CourseViewModels.Add(newCourseViewModel);
        }
        #endregion








        private void PopulateCourses()
        {
            CourseViewModel course1 = new CourseViewModel();
            course1.Course.Name = "CS 101";
            CourseViewModel course2 = new CourseViewModel();
            course2.Course.Name = "MA 101";
            CourseViewModel course3 = new CourseViewModel();
            course3.Course.Name = "PE 101";

            CourseViewModels.Add(course1);
            CourseViewModels.Add(course2);
            CourseViewModels.Add(course3);
        }
    }
}
