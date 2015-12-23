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
        public double Score
        {
            get { return score; }
            private set { score = value;}
        }

        public double MaximumScore
        {
            get { return maximumScore; }
            private set { maximumScore = value; }
        }
        #endregion

        #region Constructors
        public ComputedGrade() : base() { }

        public ComputedGrade(double score, double maximumScore): base(score, maximumScore) { }
        #endregion
    }
}
