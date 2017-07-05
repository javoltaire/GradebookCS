using GradebookCS.Common;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.ViewModel
{
    public class AssignmentViewModel : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Boolean indicating whether this assignment is being edited
        /// </summary>
        private bool isInEditMode = false;
        #endregion

        #region Properties
        /// <summary>
        /// The assignment for this assignment view model
        /// </summary>
        public Assignment Assignment { get; private set; } = new Assignment();

        /// <summary>
        /// Gets or Sets the boolean indicating whether this assignment is being edited
        /// </summary>
        /// <value>Boolean indicating whether this assignment is being edited</value>
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                if (value != isInEditMode)
                {
                    isInEditMode = value;
                    onPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to initialize a new instance of this class
        /// </summary>
        /// <param name="assignment">the assignment for this view model</param>
        public AssignmentViewModel(Assignment assignment)
        {
            this.Assignment = assignment;
        }
        #endregion
    }
}
