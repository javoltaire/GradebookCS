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
    public class Assignment : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Store for the <see cref="Id"/> Property
        /// </summary>
        private string id = Guid.NewGuid().ToString();

        /// <summary>
        /// Store for the <see cref="ComponentId"/> property
        /// </summary>
        private string componentId = default(string);

        /// <summary>
        /// Store for the <see cref="Name"/> Property
        /// </summary>
        private string name = string.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the ID of the assignment
        /// </summary>
        /// <value>The Id of the assignment</value>
        public string Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the ID of the parent component
        /// </summary>
        /// <value>The ID of the component that holds this assigment</value>
        public string ComponentId
        {
            get { return componentId; }
            set
            {
                if (value != componentId)
                {
                    componentId = value;
                    onPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or Sets the name of the assignment
        /// </summary>
        /// <value>The name of the assignment</value>
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
        /// <value>The grade of the assignment</value>
        public AdjustableGrade Grade { get; private set; } = new AdjustableGrade();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Assignment class 
        /// </summary>
        public Assignment() { }

        /// <summary>
        /// Initializes an instance of the Assignment class with the given values
        /// </summary>
        /// <param name="id">The id for the assingment</param>
        /// <param name="componentId">The id for the parent component</param>
        /// <param name="name">The name of the assignment</param>
        /// <param name="score">The score of the assignment</param>
        /// <param name="maximumScore">The maximum score of the assignment</param>
        public Assignment(string id, string componentId, string name, double score, double maximumScore)
        {
            this.Id = id;
            this.ComponentId = componentId;
            this.Name = name;
            this.Grade = new AdjustableGrade(score, maximumScore);
        }
        #endregion
    }
}
