using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// Represents a Grade where the score and maxScore is calculated
    /// </summary>
    public class ComputedGrade : GradeBase
    {
        #region Properties
        /// <summary>
        /// Gets the score of the grade
        /// </summary>
        public double Score
        {
            get { return score; }
            private set { score = value;}
        }

        /// <summary>
        /// Gets the maximum score of the grade
        /// </summary>
        public double MaximumScore
        {
            get { return maximumScore; }
            private set { maximumScore = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the ComputedGrade class with default values
        /// </summary>
        public ComputedGrade() : base(0,0) { }

        /// <summary>
        /// Initializes an instance of the ComputedGrade class with given values
        /// </summary>
        /// <param name="score">The initial score</param>
        /// <param name="maximumScore">The initail maximum score</param>
        public ComputedGrade(double score, double maximumScore): base(score, maximumScore) { }
        #endregion

        #region Methods
        /// <summary>
        /// Scales the <see cref="score"/> of the grade based on the given value
        /// </summary>
        /// <param name="scaleValue">The maximum score to scale the <see cref="Score"/> to</param>
        /// <returns>A new <see cref="ComputedGrade"/> with the scaled values</returns>
        public ComputedGrade Scale(double scaleValue)
        {
            if (scaleValue > 0 && maximumScore > 0)
            {
                double scaledScore = scaleValue * score / maximumScore;
                return new ComputedGrade(scaledScore, scaleValue);
            }
            return new ComputedGrade();
        }
        #endregion


        
    }
}
