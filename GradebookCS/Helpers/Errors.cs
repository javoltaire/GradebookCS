using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Common
{
    /// <summary>
    /// Represents an error when trying to update properties
    /// </summary>
    public class Errors : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Storing field for the error message
        /// </summary>
        private string errorMessage = string.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the error message
        /// </summary>
        /// <remarks>
        /// Before setting the new value of the error message, the value is checked to make sure that it is different
        /// From what it was before. If it is different then set the new value then notify that the error message and 
        /// the <see cref="IsSet"/> Property has changed.
        /// </remarks>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (value != errorMessage)
                {
                    errorMessage = value;
                    onPropertyChanged();
                    onPropertyChanged("IsSet");
                }
            }
        }

        /// <summary>
        /// Tell whether the error message is set, meaning that there is an error
        /// </summary>
        /// <remarks>
        /// This property just checks if the error message is null, empty, or white spaces, and return the result of that.
        /// </remarks>
        public bool IsSet { get { return String.IsNullOrWhiteSpace(errorMessage); } }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Errors() { }
        #endregion
    }
}
