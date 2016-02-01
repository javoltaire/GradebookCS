using GradebookCS.Common;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.ViewModel.UserControlsViewModels
{
    public class ComponentViewModel : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// States whether or not the component view should be in edit mode
        /// </summary>
        private bool isInEditMode = false;
        #endregion
        #region Properties
        #region Other Properties
        public Component Component { get; private set; } = new Component();
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

        public ObservableCollection<AssignmentViewModel> AssignmentViewerViewModels { get; private set; } = new ObservableCollection<AssignmentViewModel>();
        #endregion

        #endregion

        #region Constructors
        public ComponentViewModel() { }
        #endregion

        #region to be deleted
        #endregion


    }
}
