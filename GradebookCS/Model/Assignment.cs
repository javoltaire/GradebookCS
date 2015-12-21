using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    /// <summary>
    /// This class represent an Assignment
    /// </summary>
    /// <example>
    /// An <see cref="Assignment"/> could be something like a homework, a quiz or a test
    /// <code>
    /// Assignment test1 = new Assignment("Test 1", new Grade(95, 100));
    /// </code>
    /// </example>
    public class Assignment
    {
        #region Properties
        /// <summary>
        /// Name Property
        /// </summary>
        /// <value>
        /// Name of the <see cref="Assignment"/>
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Grade Property
        /// </summary>
        /// <value>
        /// The grade of the <see cref="Assignment"/>
        /// </value>
        public Grade Grade { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Assignment()
        {
            Name = "";
            Grade = new Grade();
        }

        /// <summary>
        /// Constructor to initialize properties and attributes with the given values
        /// </summary>
        /// <param name="name">Name of the <see cref="Assignment"/></param>
        /// <param name="score">Score of the <see cref="Grade"/> in the <see cref="Assignment"/></param>
        /// <param name="maximumScore">MaximumScore of the <see cref="Grade"/> in the <see cref="Assignment"/></param>
        public Assignment(string name, double score, double maximumScore)
        {
            Name = name;
            Grade = new Grade(score, maximumScore);
        }

        /// <summary>
        /// Constructor to initialize properties and attributes with the given values
        /// </summary>
        /// <param name="name">Name of the <see cref="Assignment"/></param>
        /// <param name="grade">Grade of the <see cref="Assignment"/></param>
        public Assignment(string name, Grade grade)
        {
            Name = name;
            Grade = grade;
        }
        #endregion
    }
}
