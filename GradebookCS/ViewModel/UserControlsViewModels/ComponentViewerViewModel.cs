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
    public class ComponentViewerViewModel : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// States whether or not the component view should be in edit mode
        /// </summary>
        private bool isInEditMode;
        #endregion
        #region Properties
        #region Command Properties
        public RelayCommand ShowComponentEditingControlsCommand { get; private set; }
        public RelayCommand HideComponentEditingControlsCommand { get; private set; }
        #endregion
        #region Other Properties
        public Component Component { get; private set; }
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

        public ObservableCollection<AssignmentViewerViewModel> AssignmentViewerViewModels { get; private set; }
        #endregion

        #endregion

        #region Constructors
        public ComponentViewerViewModel()
        {
            Component = new Component();
            AssignmentViewerViewModels = new ObservableCollection<AssignmentViewerViewModel>();
            isInEditMode = false;
            ShowComponentEditingControlsCommand = new RelayCommand(() => IsInEditMode = true, () => true);
            HideComponentEditingControlsCommand = new RelayCommand(() => IsInEditMode = false, () => true);
        }
        #endregion

        #region to be deleted
        #endregion
    }
}
