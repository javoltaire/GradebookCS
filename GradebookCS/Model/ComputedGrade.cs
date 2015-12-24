using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class ComputedGrade : GradeBase
    {
        #region Properties
        /// <summary>
        /// Gets or Sets the Score, outside the class, only gets the score
        /// </summary>
        public double Score
        {
            get { return score; }
            private set { score = value;}
        }

        /// <summary>
        /// Gets or Sets the Maximum Score, outside the class, only gets the maximum score
        /// </summary>
        public double MaximumScore
        {
            get { return maximumScore; }
            private set { maximumScore = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to instantiate this class and initialize its attributes ans properties with default values
        /// </summary>
        public ComputedGrade() : base(0,0) { }

        /// <summary>
        /// Constructor to instantiate this class and initialize its attributes ans properties with given values
        /// </summary>
        /// <param name="score">The given initial score</param>
        /// <param name="maximumScore">The given initail maximum score</param>
        public ComputedGrade(double score, double maximumScore): base(score, maximumScore) { }
        #endregion

        #region Methods
        /// <summary>
        /// Scales the <see cref="score"/> of the grade based on the given value
        /// </summary>
        /// <param name="scaleValue">The maximum score to scale the <see cref="score"/> to</param>
        /// <returns>A new <see cref="ComputedGrade"/> with the scaled values</returns>
        public ComputedGrade Scale(double scaleValue)
        {
            double scaledScore = scaleValue * score / maximumScore;
            return new ComputedGrade(scaledScore, scaleValue);
        }
        #endregion
    }
}
