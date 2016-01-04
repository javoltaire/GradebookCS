using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradebookCS.ViewModel.Commands
{
    public class ViewCourseDetailsCommand : ICommand
    {
        private CourseListPageViewModel courseListPageViewModel;
        public ViewCourseDetailsCommand(CourseListPageViewModel courseListPageViewModel)
        {
            this.courseListPageViewModel = courseListPageViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            courseListPageViewModel.ViewCourseDetails();
        }
    }
}
