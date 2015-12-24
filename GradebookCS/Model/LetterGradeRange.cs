using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// This class represent a Range for a grade
    /// </summary>
    /// <example>
    /// For Instance, From 90 to 100 is an A.
    /// <code>
    /// LetterGradeRange aRange = new LetterGradeRange("A", 90, 100);
    /// </code>
    /// </example>
    public class LetterGradeRange
    {
        #region Attributes
        /// <summary>
        /// The letter associated with this <see cref="LetterGradeRange"/>
        /// </summary>
        private string letter = "";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the <see cref="letter"/> of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The <see cref="letter"/> Associated with the <see cref="LetterGradeRange"/>.</value>
        /// <exception cref="ArgumentException">Thrown when the value for letter has more than 2 characters</exception>
        /// <exception cref="ArgumentNullException">Thrown when the value has more than two characters.</exception>
        public string Letter
        {
            get { return letter; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Letter Should not be empty, null, or WhiteSpaces");
                else if(value.Length > 2)
                    throw new ArgumentException("Letter should not have more than 2 characters"); 
                else
                    letter = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="lowEnd"/> of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The <see cref="lowEnd"/> of the <see cref="LetterGradeRange"/>.</value>
        public double LowEnd { get; private set; } = 0;

        /// <summary>
        /// Gets or sets the <see cref="highEnd"/> of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The <see cref="highEnd"/> of the <see cref="LetterGradeRange"/>.</value>
        public double HighEnd { get; private set; } = 1;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor for creating an instance of this class
        /// </summary>
        public LetterGradeRange() { }

        /// <summary>
        /// Constructor to initialize Attributes and properties for an instance with the given values
        /// </summary>
        /// <param name="letter">The given letter for the <see cref="LetterGradeRange"/></param>
        /// <param name="lowEnd">The given lowEnd for the <see cref="LetterGradeRange"/></param>
        /// <param name="highEnd">The given highEnd for the <see cref="LetterGradeRange"/></param>
        public LetterGradeRange(string letter, double lowEnd, double highEnd)
        {
            Letter = letter;
            this.LowEnd = lowEnd;
            this.HighEnd = highEnd;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the values of the <see cref="LetterGradeRange"/> with the new given values
        /// </summary>
        /// <param name="newLowEnd">The new <see cref="lowEnd"/> value</param>
        /// <param name="newHighEnd">The new <see cref="highEnd"/> value</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown When the new values are negative</exception>
        /// <exception cref="ArgumentException">Throw when the LowEnd argument value is greater than the highEnd argument</exception>
        public void updateRangeValues(double newLowEnd, double newHighEnd)
        {
            if (newLowEnd < 0)
                throw new ArgumentOutOfRangeException("Values cannot be Negative");
            else if (newLowEnd > newHighEnd)
                throw new ArgumentException("Low End has to be less than High end");
            else
            {
                this.LowEnd = newLowEnd;
                this.HighEnd = newHighEnd;
            }
        }
        /// <summary>
        /// Determines whether a given value is between the <see cref="lowEnd"/> and <see cref="highEnd"/>
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value is between <see cref="lowEnd"/> and <see cref="highEnd"/>, and false otherwise.</returns>
        public bool IsInRange(double value)
        {
            return value >= LowEnd && value <= HighEnd;
        }
        #endregion
        
    }


}
