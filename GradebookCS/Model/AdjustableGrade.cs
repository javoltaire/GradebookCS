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
        public double Score
        {
            get { return score; }
            set { score = value; }
        }

        public double MaximumScore
        {
            get { return maximumScore; }
            set { maximumScore = value; }
        }
        #endregion

        #region Constructors
        public AdjustableGrade() : base() { }

        public AdjustableGrade(double score, double maximumScore): base(score, maximumScore) { }
        #endregion
    }
}
