using System.Collections.ObjectModel;
using GradebookCS.Common;
using System.Collections.Specialized;
using System;

namespace GradebookCS.Model
{
    /// <summary>
    /// This class represents a Course that is made up of <see cref="Component"/>s
    /// </summary>
    public class Course: BaseINPC
    {
        #region Attributes
        /// <summary>
        /// Store for Id property
        /// </summary>
        private string id = Guid.NewGuid().ToString();

        /// <summary>
        /// Store for the <see cref="Name"/> Property
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// Store the boolean value stating whether or not actual score or percentage should be used to determine the letter grade
        /// </summary>
        private bool usePercent = true;

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
        /// Gets or Sets the id of the course
        /// </summary>
        /// <value>The id for the course</value>
        public string Id
        {
            get { return id; }
            set
            {
                if(value != id)
                {
                    id = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the name of this course
        /// </summary>
        /// <value>The name of the course</value>
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
        /// Gets or Sets the boolean stating whether or not the percentage should be used to calculate the Letter
        /// </summary>
        /// <value>The boolean stating whether or not the percentage should be used to calculate the Letter</value>
        public bool UsePercent
        {
            get { return usePercent; }
            set
            {
                if(value != usePercent)
                {
                    usePercent = value;
                    onPropertyChanged();
                    onPropertyChanged("Letter");
                }
            }
        }

        /// <summary>
        /// Gets or Sets the low end of the A range
        /// </summary>
        /// <value>The Low end of the A range</value>
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
        /// <value>The High end of the A range</value>
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
        /// <value>The Low end of the B range</value>
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
        /// <value>The High end of the B range</value>
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
        /// <value>The Low end of the C range</value>
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
        /// <value>The High end of the C range</value>
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
        /// <value>The Low end of the NR range</value>
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
        /// <value>The High end of the NR range</value>
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
                if (usePercent)
                {
                    double tempScorePercent = Grade.Percent;
                    return ComputeLetterFor(tempScorePercent);
                }
                else
                {
                    return ComputeLetterFor(Grade.Score);
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the Course class
        /// </summary>
        public Course() { }
        #endregion

        #region Methods
        /// <summary>
        /// Computes the letter for a given score value
        /// </summary>
        /// <param name="value">Score value to Compute letter for</param>
        /// <returns>The computed letter based on the given value.</returns>
        private string ComputeLetterFor(double value)
        {
            if (aRange.IsInRange(value))
                return aRange.Letter;
            else if (bRange.IsInRange(value))
                return bRange.Letter;
            else if (cRange.IsInRange(value))
                return cRange.Letter;
            else if (nrRange.IsInRange(value))
                return nrRange.Letter;
            else if (value > aRange.HighEnd)
                return aRange.Letter;
            else if (value < nrRange.LowEnd)
                return nrRange.Letter;
            else
                return "N/A";
        }
        #endregion

        #region To be refactored
        /// <summary>
        /// Listens for when a new <see cref="Component"/> is added or removed ect, and updates the <see cref="Grade"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Components_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Component component in e.NewItems)
                {
                    Grade.Add(component.WeightedGrade);
                    component.PropertyChanged += Component_PropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Component component in e.OldItems)
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

        private void Grade_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            onPropertyChanged("Letter");
        }

        /// <summary>
        /// Gets the list of <see cref="Component"/>s of this Course
        /// </summary>
        /// <value>The list of <see cref="Component"/>s for this Course</value>
        public ObservableCollection<Component> Components { get; private set; } = new ObservableCollection<Component>();

        /// <summary>
        /// Initializes an instance of the Course class
        /// </summary>
        //public Course()
        //{
            //this.Components.CollectionChanged += Components_CollectionChanged;
            //Grade.PropertyChanged += Grade_PropertyChanged;
        //}
        #endregion
    }
}
