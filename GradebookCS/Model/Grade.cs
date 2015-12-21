using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// This Class represent a grade
    /// </summary>
    /// <example>
    /// A Grade of 9 out of 10 would be represented as
    /// <code>
    /// Grade g = new Grade(9, 10)
    /// </code>
    /// </example>
    public class Grade
    {
        #region Attributes
        /// <summary>
        /// The Maximum score
        /// </summary>
        private double maximumScore = 1;
        #endregion

        #region Properties
        /// <summary>
        /// The received score Property
        /// </summary>
        /// <value>
        /// The received score</value>
        public double Score { get; set; } = 0;

        /// <summary>
        /// The Maximum Score property
        /// </summary>
        /// <remarks>
        /// Only Sets the maximum score value if the new value is greater than 0 other wise throws an exception
        /// </remarks>
        /// <value>
        /// The Maximum Score
        /// </value>
        public double MaximumScore
        {
            get { return maximumScore; }
            set
            {
                if (value < 1)
                    throw new Exception("Maximum Score cannot be 0 or negative");
                else
                    maximumScore = value;
            }
        }

        /// <summary>
        /// Percentage Property
        /// </summary>
        /// <remarks>
        /// Calculates a percentage of the grade
        /// </remarks>
        /// <example>
        /// <code>
        /// Grade g = new Grade(14, 20);
        /// double p = g.Percent
        /// Console.writeLine(p)
        /// </code>
        /// would return 70.0
        /// </example>
        /// <value>
        /// </value>
        public double Percent
        {
            get { return 100 * Score / maximumScore; }
        }

        /// <summary>
        /// Letter property
        /// </summary>
        /// <remarks>
        /// Returns the string letter grade based on the received score and the ranges available
        /// </remarks>
        /// <value>
        /// The letter grade
        /// </value>
        public string Letter
        {
            get
            {
                if (ARange.IsInRange(Score))
                    return ARange.Letter;
                else if (BRange.IsInRange(Score))
                    return BRange.Letter;
                else if (CRange.IsInRange(Score))
                    return CRange.Letter;
                else if (NRRange.IsInRange(Score))
                    return NRRange.Letter;
                else
                    throw new Exception("Range not Found");
            }
        }

        /// <summary>
        /// A Range property
        /// </summary>
        /// <value>
        /// A Grade Range
        /// </value>
        public LetterGradeRange ARange { get; } = new LetterGradeRange("A", 90, 100);

        /// <summary>
        /// B Range property
        /// </summary>
        /// <value>
        /// B Grade Range
        /// </value>
        public LetterGradeRange BRange { get; } = new LetterGradeRange("B", 80, 89);

        /// <summary>
        /// C Range property
        /// </summary>
        /// <value>
        /// C Grade Range
        /// </value>
        public LetterGradeRange CRange { get; } = new LetterGradeRange("C", 70, 79);

        /// <summary>
        /// NR Range property
        /// </summary>
        /// <value>
        /// NR Grade Range
        /// </value>
        public LetterGradeRange NRRange { get; } = new LetterGradeRange("NR", 0, 69);
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Grade() { }

        /// <summary>
        /// Constructor to initialize attributes and properties with the given values
        /// </summary>
        /// <param name="score">The score for the grade</param>
        /// <param name="maximumScore">The maximum score</param>
        public Grade(double score, double maximumScore)
        {
            Score = score;
            MaximumScore = maximumScore;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Scales this <see cref="Grade"/> to a given value
        /// </summary>
        /// <example>
        /// <code>
        /// Grade g = new Grade(90, 100)
        /// g.Scale(10)
        /// </code>
        /// would return a <see cref="Grade"/> with <see cref="Score"/> = 9 and <see cref="MaximumScore"/> = 10
        /// </example>
        /// <param name="scaleToValue">The value to scale the <see cref="Grade"/> to</param>
        /// <returns>A new <see cref="Grade"/> with the scaled values for <see cref="Score"/> and <see cref="MaximumScore"/></returns>
        public Grade Scale(double scaleToValue)
        {
            double newScore = scaleToValue * Score / maximumScore;
            return new Grade(newScore, scaleToValue);
        }
        #endregion
    }
}
