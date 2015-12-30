using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class Course
    {
        #region Properties
        /// <summary>
        /// Gets or Sets the name of the <see cref="Course"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the grade for this <see cref="Course"/>
        /// </summary>
        /// <value>The Grade of the <see cref="Course"/></value>
        public ComputedGrade Grade { get; private set; }

        /// <summary>
        /// Gets or Sets the list of components of this <see cref="Course"/> can only be set from within this class
        /// </summary>
        /// <value>The list of <see cref="Component"/>s for this <see cref="Course"/></value>
        public ObservableCollection<Component> Components { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Assignment class with default values
        /// </summary>
        public Course()
        {
            Name = "Course";
            Grade = new ComputedGrade();
            Components = new ObservableCollection<Component>();
            this.Components.CollectionChanged += Components_CollectionChanged;
        }

        /// <summary>
        /// Initializes an instance of the Assignment class with given values
        /// </summary>
        /// <param name="name">The name for the course</param>
        public Course(string name)
        {
            this.Name = name;
            Grade = new ComputedGrade();
            Components = new ObservableCollection<Component>();
            this.Components.CollectionChanged += Components_CollectionChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listens for when a new <see cref="Component"/> is added or removed ect, and recalculates the grade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Components_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Grade = new ComputedGrade();
            foreach(Component c in Components)
            {
                Grade.Add(c.WeightedGrade);
            }
        }
        #endregion

    }
}
