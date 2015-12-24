using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public abstract class GradeBase
    {
        #region Attributes
        /// <summary>
        /// The received score
        /// </summary>
        protected double score;

        /// <summary>
        /// The maximim score
        /// </summary>
        protected double maximumScore;

        /// <summary>
        /// The range for an A
        /// </summary>
        private LetterGradeRange aRange;

        /// <summary>
        /// The range for a B
        /// </summary>
        private LetterGradeRange bRange;

        /// <summary>
        /// The range for a C
        /// </summary>
        private LetterGradeRange cRange;

        /// <summary>
        /// The range for an NR
        /// </summary>
        private LetterGradeRange nrRange;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the percentage of the Grade
        /// </summary>
        /// <value>The percentage, essensially  a scaled score out of 100</value>
        public double Percent
        {
            get
            {
                return 100 * score / maximumScore;
            }
        }

        /// <summary>
        /// Gets the letter Based on the score and the range for this grade
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the score isn't in between in any of the ranges.</exception>
        public string Letter
        {
            get
            {
                double tempPercent = Percent;
                if (aRange.IsInRange(tempPercent))
                    return aRange.Letter;
                else if (bRange.IsInRange(tempPercent))
                    return bRange.Letter;
                else if (cRange.IsInRange(tempPercent))
                    return cRange.Letter;
                else if (nrRange.IsInRange(tempPercent))
                    return nrRange.Letter;
                else
                    throw new ArgumentOutOfRangeException("Range not found");
            }
        }

        /// <summary>
        /// Gets the low end of the A range
        /// </summary>
        public double ARangeLowEnd { get { return aRange.LowEnd; } }

        /// <summary>
        /// Gets the High end of the A range
        /// </summary>
        public double ARangeHighEnd { get { return aRange.HighEnd; } }

        /// <summary>
        /// Gets the low end of the B range
        /// </summary>
        public double BRangeLowEnd { get { return bRange.LowEnd; } }

        /// <summary>
        /// Gets the High end of the B range
        /// </summary>
        public double BRangeHighEnd { get { return bRange.HighEnd; } }

        /// <summary>
        /// Gets the low end of the C range
        /// </summary>
        public double CRangeLowEnd { get { return cRange.LowEnd; } }

        /// <summary>
        /// Gets the High end of the C range
        /// </summary>
        public double CRangeHighEnd { get { return cRange.HighEnd; } }

        /// <summary>
        /// Gets the low end of the NR range
        /// </summary>
        public double NRRangeLowEnd { get { return nrRange.LowEnd; } }

        /// <summary>
        /// Gets the High end of the NR range
        /// </summary>
        public double NRRangeHighEnd { get { return nrRange.HighEnd; } }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with default values
        /// </summary>
        protected GradeBase()
        {
            score = 0;
            maximumScore = 1;
            aRange = new LetterGradeRange("A", 90, 100);
            bRange = new LetterGradeRange("B", 80, 89);
            cRange = new LetterGradeRange("C", 70, 79);
            nrRange = new LetterGradeRange("NR", 0, 69);
        }

        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with the given values
        /// </summary>
        /// <param name="score">The score of the grade</param>
        /// <param name="maximumScore">The Maximum score of the grade</param>
        protected GradeBase(double score, double maximumScore)
        {
            this.score = score;
            this.maximumScore = maximumScore;
            aRange = new LetterGradeRange("A", 90, 100);
            bRange = new LetterGradeRange("B", 80, 89);
            cRange = new LetterGradeRange("C", 70, 79);
            nrRange = new LetterGradeRange("NR", 0, 69);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds up another grade to this grade
        /// </summary>
        /// <remarks>Adds up the scores together and maxscores together</remarks>
        /// <param name="grade">The grade to add to this</param>
        public void Add(GradeBase grade)
        {
            this.score += grade.score;
            this.maximumScore += grade.maximumScore;
        }

        /// <summary>
        /// Updates The values of all the <see cref="LetterGradeRange"/>s
        /// </summary>
        /// <param name="aRangeLow">The low end of the A Range</param>
        /// <param name="aRangeHigh">The high end of the A Range</param>
        /// <param name="bRangeLow">The low end of the B Range</param>
        /// <param name="bRangeHigh">The high end of the B Range</param>
        /// <param name="cRangeLow">The low end of the C Range</param>
        /// <param name="cRangeHigh">The high end of the C Range</param>
        /// <param name="nrRangeLow">The low end of the NR Range</param>
        /// <param name="nrRangeHigh">The high end of the NR Range</param>
        public void UpdateRangeValue(double aRangeLow, double aRangeHigh, double bRangeLow,
            double bRangeHigh, double cRangeLow, double cRangeHigh, double nrRangeLow, double nrRangeHigh)
        {
            aRange.updateRangeValues(aRangeLow, aRangeHigh);
            bRange.updateRangeValues(bRangeLow, bRangeHigh);
            cRange.updateRangeValues(cRangeLow, cRangeHigh);
            nrRange.updateRangeValues(nrRangeLow,nrRangeHigh);
        }
        #endregion
    }
}
