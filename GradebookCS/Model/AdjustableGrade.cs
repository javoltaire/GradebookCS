using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// An Ajustable grade is a grade where the score and max can be changed by the user.
    /// </summary>
    /// <example>
    /// A grade of 90 out of 100 would be represented as:
    /// <code>AdjustableGrade grade = new AdjustableGrade(90, 100);</code>
    /// </example>
    public class AdjustableGrade : GradeBase
    {
        #region Properties
        /// <summary>
        /// Gets or Sets the Score
        /// </summary>
        /// <value>The score of the grade</value>
        public double Score
        {
            get { return score; }
            set
            {
                if (value != score)
                {
                    score = value;
                    onPropertyChanged();
                    onPropertyChanged("StringScoreMaxScore");
                    onPropertyChanged("Percent");
                    onPropertyChanged("PercentWithSign");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Maximum score
        /// </summary>
        /// <value>The maximum score of the grade</value>
        public double MaximumScore
        {
            get { return maximumScore; }
            set
            {
                if (value != maximumScore)
                {
                    maximumScore = value;
                    onPropertyChanged();
                    onPropertyChanged("StringScoreMaxScore");
                    onPropertyChanged("Percent");
                    onPropertyChanged("PercentWithSign");
                }
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an istance of the AdjustableGrade class with default values
        /// </summary>
        public AdjustableGrade() : base(0, 100) { }

        /// <summary>
        /// Initializes an instance of the AdjustableGrade class with given values
        /// </summary>
        /// <param name="score">The given initial score</param>
        /// <param name="maximumScore">The given initial maximum score</param>
        public AdjustableGrade(double score, double maximumScore) : base(score, maximumScore) { }
        #endregion

        #region Methods
        /// <summary>
        /// Scales the <see cref="Score"/> of the grade based on the given value
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
