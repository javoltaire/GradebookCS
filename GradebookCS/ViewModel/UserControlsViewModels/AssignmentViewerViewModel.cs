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
        private bool isInEditMode;
        #endregion

        #region Properties
        public Assignment Assignment { get; private set; }

        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                if(value != isInEditMode)
                {
                    isInEditMode = value;
                    onPropertyChanged();
                }
            }
        }

        
        public RelayCommand ShowAssignmentEditingConstolsCommand { get; private set; }
        public RelayCommand HideAssignmentEditingConstolsCommand { get; private set; }
        #endregion

        #region Constructors
        public AssignmentViewerViewModel()
        {
            Assignment = new Assignment();
            isInEditMode = false;
            ShowAssignmentEditingConstolsCommand = new RelayCommand(() => IsInEditMode = true, () => true);
            HideAssignmentEditingConstolsCommand = new RelayCommand(() => IsInEditMode = false, () => true);
        }
        #endregion
    }
}
