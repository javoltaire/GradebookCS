using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// This Class represent a range for a <see cref=LetterGrade/>.
    /// </summary>
    /// <example>
    /// Am A is from 90 to 100
    /// <code>LetterGradeRange ARange = new LetterGradeRange</code>
    /// </example>
    public class LetterGradeRange
    {
        #region Attributes
        /// <summary>
        /// The low end of the <see cref="LetterGradeRange"/>.
        /// </summary>
        private double lowEnd = 0;

        /// <summary>
        /// The high end of the <see cref="LetterGradeRange"/>.
        /// </summary>
        private double highEnd = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Letter Property
        /// </summary>
        /// <value>
        /// The letter associated with the <see cref="LetterGradeRange"/>.
        /// </value>
        public string Letter { get; set; } = "";

        /// <summary>
        /// The low end property
        /// </summary>
        /// <remarks>
        /// When setting the low end value, it first make sure the new value isn't 0 or greater than the high end or will throw exceptions
        /// </remarks>
        /// <value>
        /// The low end of the <see cref="LetterGradeRange"/>.
        /// </value>
        public double LowEnd {
            get { return lowEnd; }
            set
            {
                if (value < 0)
                    throw new Exception("Low end Range Cannot be negative");
                else if (value > highEnd)
                    throw new Exception("Low end of the range cannot be greater than the high end");
                else
                    lowEnd = value;
            }
        }

        /// <summary>
        /// The high end property
        /// </summary>
        /// <remarks>
        /// When setting the high end value, it first make sure the new value isn't less than 0, and isn't less than the low end</remarks>
        /// <value>
        /// The high end of the <see cref="LetterGradeRange"/>.
        /// </value>
        public double HighEnd
        {
            get { return highEnd; }
            set
            {
                if (value < 0)
                    throw new Exception("High end Range Cannot be negative");
                else if (value < highEnd)
                    throw new Exception("High end of the range cannot be less than the low end");
                else
                    highEnd = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LetterGradeRange() { }

        /// <summary>
        /// Constructor to initialize properties with the given values
        /// </summary>
        /// <param name="letter">The associated letter for this <see cref="LetterGradeRange"/>.</param>
        /// <param name="lowEnd">The low end of this <see cref="LetterGradeRange"/>.</param>
        /// <param name="highEnd">The high end of this <see cref="LetterGradeRange"/>.</param>
        public LetterGradeRange(string letter, double lowEnd, double highEnd)
        {
            Letter = letter;
            LowEnd = lowEnd;
            HighEnd = highEnd;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determins whether or not a number is between the low end and high end of this range
        /// </summary>
        /// <param name="score">The number to check</param>
        /// <returns>True if the number is betwee low end and high end, False otherwise</returns>
        public bool IsInRange(double score)
        {
            return score >= lowEnd && score <= highEnd;
        }
        #endregion
    }
}
