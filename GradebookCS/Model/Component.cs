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
        /// The weight for this component
        /// </summary>
        /// <remarks>weight is how much is this component worth for a <see cref="Course"/></remarks>
        private double weight = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of this <see cref="Component"/>
        /// </summary>
        /// <value>The name of Component</value>
        public string Name { get; set; }

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
        /// Gets the Total grade
        /// </summary>
        public ComputedGrade TotalGrade { get; private set; }

        /// <summary>
        /// Gets the calculated Weighted grade
        /// </summary>
        public ComputedGrade WeightedGrade { get { return TotalGrade.Scale(Weight); } }

        /// <summary>
        /// Gets the list that hold all the assignments
        /// </summary>
        public ObservableCollection<Assignment> Assignments { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Component class with the default values
        /// </summary>
        public Component()
        {
            Name = "Component";
            weight = 100;
            TotalGrade = new ComputedGrade();
            Assignments = new ObservableCollection<Assignment>();
            Assignments.CollectionChanged += Assignments_CollectionChanged;
        }

        /// <summary>
        /// Initializes an instance of the Component class with the given values
        /// </summary>
        /// <param name="name">The name of the component</param>
        /// <param name="weight">The weight of the component</param>
        public Component(string name, double weight)
        {
            this.Name = name;
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
                //TotalGrade.Add(assignment.Grade);
            }
        }
        #endregion
    }
}
