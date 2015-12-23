using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class Course
    {
        #region Attributes
        private string name;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                    throw new Exception("Name cannot be empty");
                else if (!name.Equals(value))
                {
                    name = value;
                }
            }
        }

        public ComputedGrade Grade { get; private set; }

        public ObservableCollection<Component> Components { get; private set; }
        #endregion

        #region Constructors
        public Course()
        {
            name = "Course";
            Grade = new ComputedGrade();
            Components = new ObservableCollection<Component>();
        }

        public Course(string name)
        {
            Name = name;
            this.Grade = new ComputedGrade();
            Components = new ObservableCollection<Component>();
        }
        #endregion





    }
}
