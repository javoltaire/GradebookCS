﻿using GradebookCS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// An abstract class to Represent a Grade, Something like 90 out of 100.
    /// </summary>
    public abstract class GradeBase : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// The received score
        /// </summary>
        protected double score;

        /// <summary>
        /// The maximim score possible
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
        /// <value>The percentage, essensially a scaled score out of 100</value>
        public double Percent
        {
            get
            {
                if (maximumScore < 1 || score < 0)
                    return 0;
                return 100 * score / maximumScore;
            }
        }

        public string PercentWithSign { get { return Percent + "%"; } }

        /// <summary>
        /// Gets the letter based on the percentage and the ranges for this grade
        /// </summary>
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
                    return "N/A";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String StringScoreMaxScore
        {
            get { return score + " / " + maximumScore; }
        }

        /// <summary>
        /// Gets or Sets the low end of the A range
        /// </summary>
        public double ARangeLowEnd
        {
            get { return aRange.LowEnd; }
            set
            {
                if (value != aRange.LowEnd)
                {
                    aRange.LowEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the A range
        /// </summary>
        public double ARangeHighEnd
        {
            get { return aRange.HighEnd; }
            set
            {
                if (value != aRange.HighEnd)
                {
                    aRange.HighEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the B range
        /// </summary>
        public double BRangeLowEnd
        {
            get { return bRange.LowEnd; }
            set
            {
                if (value != bRange.LowEnd)
                {
                    bRange.LowEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the B range
        /// </summary>
        public double BRangeHighEnd
        {
            get { return bRange.HighEnd; }
            set
            {
                if (value != bRange.HighEnd)
                {
                    bRange.HighEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the C range
        /// </summary>
        public double CRangeLowEnd
        {
            get { return cRange.LowEnd; }
            set
            {
                if (value != cRange.LowEnd)
                {
                    cRange.LowEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the C range
        /// </summary>
        public double CRangeHighEnd
        {
            get { return cRange.HighEnd; }
            set
            {
                if (value != cRange.HighEnd)
                {
                    cRange.HighEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the NR range
        /// </summary>
        public double NRRangeLowEnd
        {
            get { return nrRange.LowEnd; }
            set
            {
                if (value != nrRange.LowEnd)
                {
                    nrRange.LowEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the NR range
        /// </summary>
        public double NRRangeHighEnd
        {
            get { return nrRange.HighEnd; }
            set
            {
                if (value != nrRange.HighEnd)
                {
                    nrRange.HighEnd = value;
                    onPropertyChanged("Letter");
                }
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the GradeBase class with default values
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
        /// Initializes an instance of the GradeBase class with given values
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
    }
}
