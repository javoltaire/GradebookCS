using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradebookCS.ViewModel.Commands
{
    public class AddNewCourseCommand : ICommand
    {
        #region Attributes
        private CourseListPageViewModel courseListPageViewModel;
        #endregion

        #region Constructors
        public AddNewCourseCommand(CourseListPageViewModel courseListPageViewModel)
        {
            this.courseListPageViewModel = courseListPageViewModel;
        }
        #endregion

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            courseListPageViewModel.AddNewCourse();
        }
    }
}
