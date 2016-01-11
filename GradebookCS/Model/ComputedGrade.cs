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
    /// <example>
    /// A grade of 90 out of 100 would be represented as:
    /// <code>ComputedGrade grade = new ComputedGrade(90, 100);</code>
    /// </example>
    public class ComputedGrade : GradeBase
    {
        #region Properties
        /// <summary>
        /// Gets the score of the grade
        /// </summary>
        /// <value>The score of the grade</value>
        public double Score
        {
            get { return score; }
            private set
            {
                if(value != score)
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
        /// Gets the maximum score of the grade
        /// </summary>
        /// <value>The maximum score of the grade</value>
        public double MaximumScore
        {
            get { return maximumScore; }
            private set
            {
                if(value != maximumScore)
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
        /// Initializes an instance of the ComputedGrade class with default values
        /// </summary>
        public ComputedGrade() : base(0.0,0.0) { }

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
        /// <returns>A new Computed Grade with the scaled values</returns>
        //public ComputedGrade Scale(double scaleValue)
        //{
        //    if (scaleValue > 0.0 && maximumScore > 0.0)
        //    {
        //        double scaledScore = scaleValue * score / maximumScore;
        //        return new ComputedGrade(scaledScore, scaleValue);
        //    }
        //    return new ComputedGrade();
        //}

        /// <summary>
        /// Adds/Sums up another <see cref="AdjustableGrade"/> to this grade
        /// </summary>
        /// <remarks>Adds up the scores together and maxscores together</remarks>
        /// <param name="grade">The grade to sum up with this grade</param>
        public void Add(AdjustableGrade grade)
        {
            this.Score += grade.Score;
            this.MaximumScore += grade.MaximumScore;
        }

        /// <summary>
        /// Adds/Sums up another ComputedGrade to this grade
        /// </summary>
        /// <remarks>Adds up the scores together and maxscores together</remarks>
        /// <param name="grade">The grade to sum up with this grade</param>
        public void Add(ComputedGrade grade)
        {
            this.Score += grade.Score;
            this.MaximumScore += grade.MaximumScore;
        }

        /// <summary>
        /// Substract another <see cref="AdjustableGrade"/> from this grade
        /// </summary>
        /// <param name="grade">The grade to Substract from this grade</param>
        public void Subtract(AdjustableGrade grade)
        {
            if(maximumScore >= 0.0)
            {
                this.Score -= grade.Score;
                this.MaximumScore -= grade.MaximumScore;
            }
        }

        /// <summary>
        /// Substract another ComputedGrade from this grade
        /// </summary>
        /// <param name="grade">The grade to Substract from this grade</param>
        public void Subtract(ComputedGrade grade)
        {
            if (maximumScore >= 0.0)
            {
                this.Score -= grade.Score;
                this.MaximumScore -= grade.MaximumScore;
            }
        }

        /// <summary>
        /// Resets this grade by setting the score and the maximum score to 0
        /// </summary>
        public void Reset()
        {
            this.score = 0.0;
            this.maximumScore = 0.0;
        }

        /// <summary>
        /// Resets this grade with a new <see cref="MaximumScore"/>
        /// </summary>
        /// <param name="newMaxScore"></param>
        public void Reset(double newMaxScore)
        {
            this.score = 0.0;
            this.maximumScore = newMaxScore;
        }
        #endregion
    }
}
