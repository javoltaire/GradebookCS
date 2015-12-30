using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// Represent a Range for a letter grade.
    /// </summary>
    /// <example>
    /// For Instance, From 90 to 100 is an A. And that would be represented as
    /// <code>
    /// LetterGradeRange aRange = new LetterGradeRange("A", 90, 100);
    /// </code>
    /// </example>
    public class LetterGradeRange
    {
        #region Properties
        /// <summary>
        /// Gets or Sets the letter for this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The Letter associated with this <see cref="LetterGradeRange"/>.</value>
        public string Letter { get; set; }

        /// <summary>
        /// Gets or sets the low end of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The low end of the <see cref="LetterGradeRange"/>.</value>
        public double LowEnd { get; set; }

        /// <summary>
        /// Gets or sets the high end of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The high of the <see cref="LetterGradeRange"/>.</value>
        public double HighEnd { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the LetterGradeRange class with default values
        /// </summary>
        public LetterGradeRange()
        {
            this.Letter = String.Empty;
            this.LowEnd = 0.0;
            this.HighEnd = 0.0;
        }

        /// <summary>
        /// Initializes an instance of the LetterGradeRange class with given values
        /// </summary>
        /// <param name="letter">The given letter for the <see cref="LetterGradeRange"/></param>
        /// <param name="lowEnd">The given lowEnd for the <see cref="LetterGradeRange"/></param>
        /// <param name="highEnd">The given highEnd for the <see cref="LetterGradeRange"/></param>
        public LetterGradeRange(string letter, double lowEnd, double highEnd)
        {
            this.Letter = letter;
            this.LowEnd = lowEnd;
            this.HighEnd = highEnd;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether a given value is between the <see cref="LowEnd"/> and <see cref="HighEnd"/>
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value is between <see cref="LowEnd"/> and <see cref="HighEnd"/>, and false otherwise.</returns>
        public bool IsInRange(double value)
        {
            return value >= LowEnd && value <= HighEnd;
        }
        #endregion
        
    }


}
