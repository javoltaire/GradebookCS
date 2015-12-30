using GradebookCS.Model;
using GradebookCS.ViewModel.UserControlsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradebookCS.ViewModel.Commands
{
    public class ViewCourseInfoCommand : ICommand
    {
        private CourseViewModel courseViewModel;
        public ViewCourseInfoCommand(CourseViewModel courseViewModel)
        {
            this.courseViewModel = courseViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            courseViewModel.ViewCourseInfo();
        }
    }
}
