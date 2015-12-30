using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.ViewModel.UserControlsViewModels
{
    public class CourseViewModel
    {
        
        public Course Course { get; private set; }
        public ViewCourseInfoCommand ViewCourseInfoCommand { get; private set; }


        public CourseViewModel()
        {
            Course = new Course();

            ViewCourseInfoCommand = new ViewCourseInfoCommand(this);
        }

        public void ViewCourseInfo()
        {
            CourseInfoDialogViewModel dialogViewModel = new CourseInfoDialogViewModel(Course);
            //dialogViewModel.ShowDialog();
        }
    }
}
