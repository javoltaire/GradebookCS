using GradebookCS.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.Model
{
    /// <summary>
    /// This class represents a component of course like Homework, Quizzes or Tests, etc
    /// </summary>
    public class Component : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Store for the <see cref="Name"/> Property
        /// </summary>
        private string name = "Component";

        /// <summary>
        /// Store for the <see cref="Weight"/> Property
        /// </summary>
        private double weight = 50.0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of this Component
        /// </summary>
        /// <value>The name of Component</value>
        public string Name
        {
            get { return name; }
            set
            {
                if(value != name)
                {
                    name = value;
                    onPropertyChanged();
                    onPropertyChanged("NameWeight");
                }
            }
        }

        /// <summary>
        /// Gets or sets the weight of this component
        /// </summary>
        /// <value>The weight of the component</value>
        /// <remarks>weight is how much is this component worth for a <see cref="Course"/></remarks>
        public double Weight
        {
            get { return weight; }
            set
            {
                if (value != weight)
                {
                    weight = value;
                    WeightedGrade = new ComputedGrade(TotalGrade.GetScaledScore(weight), weight);
                    onPropertyChanged();
                    onPropertyChanged("NameWeight");
                    onPropertyChanged("WeightedGrade");
                }
            }
        }

        /// <summary>
        /// Gets a string combination of the name and weight separated by a '-'
        /// </summary>
        public string NameWeight { get { return name + " - " + weight; } }

        /// <summary>
        /// Gets the Total grade
        /// </summary>
        /// <value>The total unweighted grade</value>
        public ComputedGrade TotalGrade { get; private set; } = new ComputedGrade();

        /// <summary>
        /// Gets the calculated Weighted grade
        /// </summary>
        /// <value>The weighted grade</value>
        public ComputedGrade WeightedGrade { get; private set; }

        /// <summary>
        /// Gets the list that hold all the assignments
        /// </summary>
        /// <value>The list containing all the assignments</value>
        public ObservableCollection<Assignment> Assignments { get; private set; } = new ObservableCollection<Assignment>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Component class
        /// </summary>
        public Component()
        {
            WeightedGrade = new ComputedGrade(0.0, weight);
            Assignments.CollectionChanged += Assignments_CollectionChanged;
            TotalGrade.PropertyChanged += TotalGrade_PropertyChanged;
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
            WeightedGrade = new ComputedGrade(0.0, weight);
            Assignments.CollectionChanged += Assignments_CollectionChanged;
            TotalGrade.PropertyChanged += TotalGrade_PropertyChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// updates the total grade whenever changes occurs in the list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The Event</param>
        private void Assignments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Assignment assignment in e.NewItems)
                {
                    TotalGrade.Add(assignment.Grade);
                    assignment.Grade.PropertyChanged += AssignmentGrade_PropertyChanged;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(Assignment assignment in e.OldItems)
                {
                    TotalGrade.Subtract(assignment.Grade);
                    assignment.Grade.PropertyChanged -= AssignmentGrade_PropertyChanged;
                }
            }
            
        }

        /// <summary>
        /// Recalculates the TotalGrade when Score or MaximumScore of the <see cref="ComputedGrade"/> of 
        /// one item in the <see cref="Assignments"/> list has changed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event</param>
        private void AssignmentGrade_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TotalGrade.Reset();
            foreach (Assignment assingment in Assignments)
            {
                TotalGrade.Add(assingment.Grade);
            }
        }

        /// <summary>
        /// Listens for changes in the properties of the <see cref="TotalGrade"/> so <see cref="WeightedGrade"/> can be updated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The Event</param>
        private void TotalGrade_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            WeightedGrade = new ComputedGrade(TotalGrade.GetScaledScore(weight), weight);
            onPropertyChanged("WeightedGrade");
        }

        #endregion
    }
}
