using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class AdjustableGrade : GradeBase
    {
        #region Properties
        /// <summary>
        /// Gets or Sets the Score
        /// </summary>
        public double Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// Gets or sets the Maximum score
        /// </summary>
        public double MaximumScore
        {
            get { return maximumScore; }
            set { maximumScore = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to instantiate this class and initialize its attributes and properties with default values
        /// </summary>
        public AdjustableGrade() : base(0, 100) { }

        /// <summary>
        /// Constructor to instantiate this class and initialize its attributes and properties with given values
        /// </summary>
        /// <param name="score">The given initial score</param>
        /// <param name="maximumScore">The given initial maximum score</param>
        public AdjustableGrade(double score, double maximumScore) : base(score, maximumScore) { }
        #endregion

        #region Methods
        /// <summary>
        /// Scales the <see cref="score"/> of the grade based on the given value
        /// </summary>
        /// <param name="scaleValue">The maximum score to scale the <see cref="score"/> to</param>
        /// <returns>A new <see cref="AdjustableGrade"/> with the scaled values</returns>
        public AdjustableGrade Scale(double scaleValue)
        {
            double scaledScore = scaleValue * score / maximumScore;
            return new AdjustableGrade(scaledScore, scaleValue);
        }
        #endregion
    }
}
