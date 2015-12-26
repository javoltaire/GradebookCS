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
        /// <summary>
        /// The name of the Assingment
        /// </summary>
        private string name = String.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the name of the assignment
        /// </summary>
        /// <remarks>
        /// The value is first checked to make sure it is not an empty string or white spaces before setting the name.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the new value for the name is empty, null, or white spaces.
        /// </exception>
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Name cannot be empty");
                else if (!name.Equals(value))
                {
                    name = value;
                }
            }
        }

        /// <summary>
        /// Grade property
        /// </summary>
        /// <remarks>
        /// Gets or sets the grade; only sets from outside the class</remarks>
        public AdjustableGrade Grade { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with the default values
        /// </summary>
        public Assignment()
        {
            this.name = "Assignment";
            this.Grade = new AdjustableGrade();
        }

        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with the given values
        /// </summary>
        /// <param name="name">The name of the assignment</param>
        public Assignment(string name)
        {
            this.Name = name;
            this.Grade = new AdjustableGrade();
        }

        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with the given values
        /// </summary>
        /// <param name="name">The name of the assignment</param>
        /// <param name="score">The score of the assignment</param>
        /// <param name="maximumScore">The maximum score of the assignment</param>
        public Assignment(string name, double score, double maximumScore)
        {
            this.name = name;
            this.Grade = new AdjustableGrade(score, maximumScore);
        }
        #endregion
    }
}
