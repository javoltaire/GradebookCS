using GradebookCS.ViewModel.UserControlsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradebookCS.ViewModel.Commands
{
    public class EditAssignmentCommand : ICommand
    {
        public AssignmentViewerViewModel assignmentViewerViewModel;

        public EditAssignmentCommand(AssignmentViewerViewModel assignmentViewerViewModel)
        {
            this.assignmentViewerViewModel = assignmentViewerViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            assignmentViewerViewModel.IsInEditMode = true;
        }
    }
}
