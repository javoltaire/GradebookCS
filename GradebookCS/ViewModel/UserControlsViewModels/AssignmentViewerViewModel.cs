using GradebookCS.Common;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.ViewModel.UserControlsViewModels
{
    public class AssignmentViewerViewModel : BaseINPC
    {
        #region Attributes
        public bool isInEditMode;
        #endregion

        #region Properties
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                isInEditMode = value;
                onPropertyChanged();
            }
        }
        public EditAssignmentCommand EditAssignmentCommand { get; private set; }
        #endregion

        #region Constructors
        public AssignmentViewerViewModel()
        {
            isInEditMode = true;
            EditAssignmentCommand = new EditAssignmentCommand(this);
        }
        #endregion
    }
}
