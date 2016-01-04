using GradebookCS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// Represent an assigment. Something like an exam, or a quizz or Homework
    /// </summary>
    /// <example>
    /// Something like an Exam assignment with a grade of 98 out of 100 would be represented as:
    /// <code>Assignment exam1 = new Assignment("Exam 1", 98, 100)</code>
    /// </example>
    public class Assignment: BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Name of the Assignment
        /// </summary>
        private string name;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the name of the assignment
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the grade of the assingment
        /// </summary>
        public AdjustableGrade Grade { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Assignment class with default values
        /// </summary>
        public Assignment()
        {
            this.Name = "Assignment";
            this.Grade = new AdjustableGrade();
        }

        /// <summary>
        /// Initializes an instance of the Assignment class with the given values
        /// </summary>
        /// <param name="name">The name of the assignment</param>
        public Assignment(string name)
        {
            this.Name = name;
            this.Grade = new AdjustableGrade();
        }

        /// <summary>
        /// Initializes an instance of the Assingment class with the given values
        /// </summary>
        /// <param name="name">The name of the assignment</param>
        /// <param name="score">The score of the assignment</param>
        /// <param name="maximumScore">The maximum score of the assignment</param>
        public Assignment(string name, double score, double maximumScore)
        {
            this.Name = name;
            this.Grade = new AdjustableGrade(score, maximumScore);
        }
        #endregion
    }
}
