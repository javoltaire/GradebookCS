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

        /// <summary>
        /// The lower part of the <see cref="LetterGradeRange"/>
        /// </summary>
        private double lowEnd = 0;

        /// <summary>
        /// The higher part of the <see cref="LetterGradeRange"/>
        /// </summary>
        private double highEnd = 1;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the <see cref="letter"/> of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The <see cref="letter"/> Associated with the <see cref="LetterGradeRange"/>.</value>
        /// <exception cref="Exception">Thrown when the value has more than two characters.</exception>
        public string Letter
        {
            get { return letter; }
            set
            {
                if (value.Length > 2)
                    throw new Exception("Letter should not have more than 2 characters");
                else
                    letter = value;
            }
        }

        /// <summary>
        /// Gets or Sets the <see cref="lowEnd"/> of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The <see cref="lowEnd"/> of the <see cref="LetterGradeRange"/>.</value>
        /// <exception cref="Exception">Thrown when the value is greater than <see cref="highEnd"/> or value is negative.</exception>
        public double LowEnd
        {
            get { return lowEnd; }
            set
            {
                if (value > highEnd || value < 0)
                    throw new Exception("Low end cannot be negative or greater than high end");
                else
                    lowEnd = value;
            }
        }

        /// <summary>
        /// Gets or Sets the <see cref="highEnd"/> of this <see cref="LetterGradeRange"/>
        /// </summary>
        /// <value> The <see cref="highEnd"/> of the <see cref="LetterGradeRange"/>.</value>
        /// <exception cref="Exception">Thrown when the value is less than <see cref="lowEnd"/> or value is negative.</exception>
        public double HighEnd
        {
            get { return highEnd; }
            set
            {
                if (value < lowEnd || value < 0)
                    throw new Exception("High end cannot be negative or less than low end");
                else
                    highEnd = value;
            }
        }
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
            LowEnd = lowEnd;
            HighEnd = highEnd;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether a given value is between the <see cref="lowEnd"/> and <see cref="highEnd"/>
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value is between <see cref="lowEnd"/> and <see cref="highEnd"/>, and false otherwise.</returns>
        public bool IsInRange(double value)
        {
            return value > lowEnd && value < highEnd;
        }
        #endregion
        
    }


}
