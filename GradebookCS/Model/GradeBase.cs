using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class GradeBase
    {
        #region Attributes
        protected double score;
        protected double maximumScore;
        private LetterGradeRange aRange;
        private LetterGradeRange bRange;
        private LetterGradeRange cRange;
        private LetterGradeRange nrRange;
        #endregion

        #region Properties
        public double Percent
        {
            get
            {
                return 100 * score / maximumScore;
            }
        }

        public string Letter
        {
            get
            {
                if (aRange.IsInRange(score))
                    return aRange.Letter;
                else if (bRange.IsInRange(score))
                    return bRange.Letter;
                else if (cRange.IsInRange(score))
                    return cRange.Letter;
                else if (nrRange.IsInRange(score))
                    return nrRange.Letter;
                else
                    throw new Exception("Range not found");
            }
        }

        public double ARangeLowEnd { get { return aRange.LowEnd; } }
        public double ARangeHighEnd { get { return aRange.HighEnd; } }
        public double BRangeLowEnd { get { return bRange.LowEnd; } }
        public double BRangeHighEnd { get { return bRange.HighEnd; } }
        public double CRangeLowEnd { get { return cRange.LowEnd; } }
        public double CRangeHighEnd { get { return cRange.HighEnd; } }
        public double NRRangeLowEnd { get { return nrRange.LowEnd; } }
        public double NRRangeHighEnd { get { return nrRange.HighEnd; } }
        #endregion

        #region Constructors
        protected GradeBase()
        {
            score = 0;
            maximumScore = 0;
            aRange = new LetterGradeRange("A", 90, 100);
            bRange = new LetterGradeRange("B", 80, 89);
            cRange = new LetterGradeRange("C", 70, 79);
            nrRange = new LetterGradeRange("NR", 0, 69);
        }

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
        public void Add(GradeBase grade)
        {
            this.score += grade.score;
            this.maximumScore += grade.maximumScore;
        }

        public void Substract(GradeBase grade)
        {
            this.score -= grade.score;
            this.maximumScore -= grade.score;
        }

        public GradeBase Scale(double scaleValue)
        {
            double scaledScore = scaleValue * score / maximumScore;
            return new GradeBase(scaledScore, scaleValue);
        }

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
