using System.Collections.ObjectModel;
using GradebookCS.Common;
using System.Collections.Specialized;

namespace GradebookCS.Model
{
    /// <summary>
    /// This class represents a Course that is made up of <see cref="Component"/>s
    /// </summary>
    public class Course: BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Store for the <see cref="Name"/> Property
        /// </summary>
        private string name = "Course";

        /// <summary>
        /// The range for an A
        /// </summary>
        private LetterGradeRange aRange = new LetterGradeRange("A", 90.0, 100.0);

        /// <summary>
        /// The range for a B
        /// </summary>
        private LetterGradeRange bRange = new LetterGradeRange("B", 80.0, 89.9);

        /// <summary>
        /// The range for a C
        /// </summary>
        private LetterGradeRange cRange = new LetterGradeRange("C", 70.0, 79.9);

        /// <summary>
        /// The range for an NR
        /// </summary>
        private LetterGradeRange nrRange = new LetterGradeRange("NR", 0.0, 69.9);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the name of this course
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
        /// Gets or Sets the low end of the A range
        /// </summary>
        public double ARangeLowEnd
        {
            get { return aRange.LowEnd; }
            set
            {
                if (value != aRange.LowEnd)
                {
                    aRange.LowEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the A range
        /// </summary>
        public double ARangeHighEnd
        {
            get { return aRange.HighEnd; }
            set
            {
                if (value != aRange.HighEnd)
                {
                    aRange.HighEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the B range
        /// </summary>
        public double BRangeLowEnd
        {
            get { return bRange.LowEnd; }
            set
            {
                if (value != bRange.LowEnd)
                {
                    bRange.LowEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the B range
        /// </summary>
        public double BRangeHighEnd
        {
            get { return bRange.HighEnd; }
            set
            {
                if (value != bRange.HighEnd)
                {
                    bRange.HighEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the C range
        /// </summary>
        public double CRangeLowEnd
        {
            get { return cRange.LowEnd; }
            set
            {
                if (value != cRange.LowEnd)
                {
                    cRange.LowEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the C range
        /// </summary>
        public double CRangeHighEnd
        {
            get { return cRange.HighEnd; }
            set
            {
                if (value != cRange.HighEnd)
                {
                    cRange.HighEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the NR range
        /// </summary>
        public double NRRangeLowEnd
        {
            get { return nrRange.LowEnd; }
            set
            {
                if (value != nrRange.LowEnd)
                {
                    nrRange.LowEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the High end of the NR range
        /// </summary>
        public double NRRangeHighEnd
        {
            get { return nrRange.HighEnd; }
            set
            {
                if (value != nrRange.HighEnd)
                {
                    nrRange.HighEnd = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets the grade for this Course
        /// </summary>
        /// <value>The Grade of the Course</value>
        public ComputedGrade Grade { get; private set; } = new ComputedGrade();

        /// <summary>
        /// Gets the letter based on the score and the ranges for this course
        /// </summary>
        public string Letter
        {
            get
            {
                double tempScorePercent = Grade.Percent;
                if (aRange.IsInRange(tempScorePercent))
                    return aRange.Letter;
                else if (bRange.IsInRange(tempScorePercent))
                    return bRange.Letter;
                else if (cRange.IsInRange(tempScorePercent))
                    return cRange.Letter;
                else if (nrRange.IsInRange(tempScorePercent))
                    return nrRange.Letter;
                else if (tempScorePercent > aRange.HighEnd)
                    return aRange.Letter;
                else
                    return "N/A";
            }
        }

        /// <summary>
        /// Gets the list of <see cref="Component"/>s of this Course
        /// </summary>
        /// <value>The list of <see cref="Component"/>s for this Course</value>
        public ObservableCollection<Component> Components { get; private set; } = new ObservableCollection<Component>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Assignment class
        /// </summary>
        public Course()
        {
            this.Components.CollectionChanged += Components_CollectionChanged;
            Grade.PropertyChanged += Grade_PropertyChanged;
        }

        private void Grade_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            onPropertyChanged("Letter");
        }

        /// <summary>
        /// Initializes an instance of the Assignment class with given values
        /// </summary>
        /// <param name="name">The name for the course</param>
        public Course(string name)
        {
            this.Name = name;
            this.Components.CollectionChanged += Components_CollectionChanged;
            Grade.PropertyChanged += Grade_PropertyChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Listens for when a new <see cref="Component"/> is added or removed ect, and updates the <see cref="Grade"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Components_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(Component component in e.NewItems)
                {
                    Grade.Add(component.WeightedGrade);
                    component.PropertyChanged += Component_PropertyChanged;
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach(Component component in e.OldItems)
                {
                    Grade.Subtract(component.WeightedGrade);
                    //component.PropertyChanged -= Component_PropertyChanged;
                }
            }
        }

        private void Component_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if(e.PropertyName == "WeightedGrade" || e.PropertyName == "Weight")
            //{

            //}
            Grade.Reset();
            foreach (Component component in Components)
            {
                Grade.Add(component.WeightedGrade);
            }
        }
        #endregion
    }
}
