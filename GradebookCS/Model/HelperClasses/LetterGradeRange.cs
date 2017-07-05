using GradebookCS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// Represents a Range for a letter grade.
    /// </summary>
    /// <example>
    /// For Instance, From 90 to 100 is an A. And that would be represented as
    /// <code>
    /// LetterGradeRange aRange = new LetterGradeRange("A", 90, 100);
    /// </code>
    /// </example>
    public class LetterGradeRange : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Store for <see cref="Letter"/> Property
        /// </summary>
        private string letter = String.Empty;

        /// <summary>
        /// Store for <see cref="LowEnd"/> Property
        /// </summary>
        private double lowEnd = 0.0;

        /// <summary>
        /// Store for <see cref="HighEnd"/> Property
        /// </summary>
        private double highEnd = 0.0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the letter associated with this Letter Grade Range
        /// </summary>
        /// <value> The Letter associated with this Letter Grade Range </value>
        public string Letter
        {
            get { return letter; }
            set {
                if (value != letter)
                {
                    letter = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the low end of this Letter Grade Range
        /// </summary>
        /// <value> The low end of the Letter Grade Range</value>
        public double LowEnd
        {
            get { return lowEnd; }
            set
            {
                if(value != lowEnd)
                {
                    lowEnd = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the high end of this Letter Grade Range
        /// </summary>
        /// <value> The high end of the Letter Grade Range</value>
        public double HighEnd
        {
            get { return highEnd; }
            set
            {
                if(value != highEnd)
                {
                    highEnd = value;
                    onPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the LetterGradeRange class
        /// </summary>
        public LetterGradeRange() { }

        /// <summary>
        /// Initializes an instance of the LetterGradeRange class with the given values
        /// </summary>
        /// <param name="letter">The letter to associate with the Letter Grade Range</param>
        /// <param name="lowEnd">The lowEnd for the Letter Grade Range</param>
        /// <param name="highEnd">The highEnd for the Letter Grade Range</param>
        public LetterGradeRange(string letter, double lowEnd, double highEnd)
        {
            this.Letter = letter;
            this.LowEnd = lowEnd;
            this.HighEnd = highEnd;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether a given value is between the <see cref="lowEnd"/> and <see cref="highEnd"/> inclusive
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value is between <see cref="LowEnd"/> and <see cref="HighEnd"/> inclusive, and false otherwise.</returns>
        public bool IsInRange(double value)
        {
            return value >= lowEnd && value <= highEnd;
        }

        #region Validation and error checking
        private bool isLetterValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.Length < 2;
        }
        #endregion
        #endregion

    }


}
