using GradebookCS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// An abstract class to Represent a Grade.
    /// </summary>
    /// <seealso cref="ComputedGrade"/>
    /// <seealso cref="AdjustableGrade"/>
    public abstract class GradeBase : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Store for the Score property in the child classes
        /// </summary>
        protected double score = 0.0;

        /// <summary>
        /// Store for the Maximum Score property in the child classes
        /// </summary>
        protected double maximumScore = 1.0;
        #endregion

        #region Properties
        /// <summary>
        /// Calulates and Gets the percentage of the Grade
        /// </summary>
        /// <value>The percentage, essentially a scaled score out of 100</value>
        public double Percent
        {
            get
            {
                if (maximumScore < 1.0 || score < 0.0)
                    return 0;
                return 100 * score / maximumScore;
            }
        }

        /// <summary>
        /// Gets a string version of the <see cref="Percent"/> Property with a added percent sign
        /// </summary>
        public string PercentWithSign { get { return Math.Round(Percent, 2) + "%"; } }

        /// <summary>
        /// A string to show the score and the max score together
        /// </summary>
        public String StringScoreMaxScore
        {
            get { return Math.Round(score, 2) + " / " + Math.Round(maximumScore, 2); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the GradeBase class
        /// </summary>
        protected GradeBase() { }

        /// <summary>
        /// Initializes an instance of the GradeBase class with given values
        /// </summary>
        /// <param name="score">The received score</param>
        /// <param name="maximumScore">The Maximum possible score</param>
        protected GradeBase(double score, double maximumScore)
        {
            this.score = score;
            this.maximumScore = maximumScore;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a value that is a scaled value of this score based on the given value
        /// </summary>
        /// <param name="scaleValue">Scale Value</param>
        /// <returns>The scaled value</returns>
        public double GetScaledScore(double scaleValue)
        {
            if (scaleValue > 0.0 && maximumScore > 0.0)
                return scaleValue * score / maximumScore;
            else return 0.0;
        }
        #endregion
    }
}
