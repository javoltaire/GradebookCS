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
    public class AssignmentViewModel : BaseINPC
    {
        #region Attributes
        private bool isInEditMode = false;
        #endregion

        #region Properties
        public Assignment Assignment { get; private set; } = new Assignment();

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
        #endregion

        #region Constructors
        public AssignmentViewModel() { }
        #endregion
    }
}
