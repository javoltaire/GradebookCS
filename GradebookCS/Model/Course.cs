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
        #region Attributes
        /// <summary>
        /// The name of the <see cref="Course"/>
        /// </summary>
        private string name;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the name of the <see cref="Course"/>
        /// </summary>
        /// <remarks>The new value is checked to make sure that it isn't an empty string or white spaces.</remarks>
        /// <value>The name of the <see cref="Course"/></value>
        /// <exception cref="ArgumentNullException">Thrown when the value to set the name to is empty, null or whitespaces</exception>
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Name cannot be empty, null or white spaces");
                else if (!name.Equals(value))
                {
                    name = value;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the grade for this <see cref="Course"/>, can only be set from within this class
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
        /// Constructor to instantiate this class and initialize attributes and properties with default values
        /// </summary>
        public Course()
        {
            name = "Course";
            Grade = new ComputedGrade();
            Components = new ObservableCollection<Component>();
            this.Components.CollectionChanged += Components_CollectionChanged;
        }

        /// <summary>
        /// Constructor to instantiate this class and initialize attributes and properties with the given values
        /// </summary>
        /// <param name="name">The name for the course</param>
        public Course(string name)
        {
            Name = name;
            this.Grade = new ComputedGrade();
            Components = new ObservableCollection<Component>();
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
