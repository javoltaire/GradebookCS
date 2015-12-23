using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class Assignment
    {
        #region Attributes
        private string name;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new Exception("Name cannot be empty");
                else if (!name.Equals(value))
                {
                    name = value;
                }
            }
        }

        public AdjustableGrade grade { get; private set; }
        #endregion

        #region Constructors
        public Assignment()
        {
            this.name = "Assignment";
            this.grade = new AdjustableGrade();
        }

        public Assignment(string name)
        {
            this.name = name;
            this.grade = new AdjustableGrade();
        }

        public Assignment(string name, double score, double maximumScore)
        {
            this.name = name;
            this.grade = new AdjustableGrade(score, maximumScore);
        }
        #endregion
    }
}
