using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class Component
    {
        #region Attributes
        /// <summary>
        /// The name of the <see cref="Component"/>
        /// </summary>
        private string name;

        /// <summary>
        /// The weight for this component
        /// </summary>
        /// <remarks>weight is how much is this component worth for a <see cref="Course"/></remarks>
        private double weight;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of this <see cref="Component"/>
        /// </summary>
        /// <remarks>Before setting the name, the new value is checked to make sure the value isn't null, empty or white spaces</remarks>
        /// <value>The name of Component</value>
        /// <exception cref="ArgumentNullException">Thrown when the new value is null, empty or whitespaces</exception>
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
        /// Gets or sets the weight of this component
        /// </summary>
        /// <value>The weight of the component</value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the new value is 0 or negative</exception>
        public double Weight
        {
            get { return weight; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Weight cannot be 0 negative");
                else
                    weight = value;
            }
        }

        /// <summary>
        /// Total grade Property
        /// </summary>
        /// <remarks>
        /// Gets or Sets the Total grade; only gets from outside the class
        /// </remarks>
        public ComputedGrade TotalGrade { get; private set; }

        /// <summary>
        /// Weighted grade Property
        /// </summary>
        /// <remarks>
        ///  Gets the calculated Weighted grade
        /// </remarks>
        public ComputedGrade WeightedGrade { get { return TotalGrade.Scale(Weight); } }

        /// <summary>
        /// Gets or sets the list that hold all the assignments; only gets from outside this class
        /// </summary>
        public ObservableCollection<Assignment> Assignments { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with the default values
        /// </summary>
        public Component()
        {
            this.name = "Component";
            weight = 100;
            TotalGrade = new ComputedGrade();
            Assignments = new ObservableCollection<Assignment>();
            Assignments.CollectionChanged += Assignments_CollectionChanged;
        }

        /// <summary>
        /// Constructor to intantiate this class and initialize its attributes and properties with the given values
        /// </summary>
        /// <param name="name">The name of the component</param>
        /// <param name="weight">The weight of the component</param>
        public Component(string name, double weight)
        {
            this.name = "Component";
            this.Weight = weight;
            TotalGrade = new ComputedGrade();
            Assignments = new ObservableCollection<Assignment>();
            Assignments.CollectionChanged += Assignments_CollectionChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Recalculates the total grade whenever changes occurs in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Assignments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TotalGrade = new ComputedGrade();
            foreach(Assignment assignment in Assignments)
            {
                TotalGrade.Add(assignment.Grade);
            }
        }
        #endregion
    }
}
