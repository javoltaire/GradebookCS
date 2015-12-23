using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class Component
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
        public double Weight { get; set; }
        public ComputedGrade TotalGrade { get; }
        public ComputedGrade WeightedGrade { get { return (ComputedGrade) TotalGrade.Scale(Weight); } }
        public ObservableCollection<Assignment> Assignments { get; private set; }
        #endregion

        #region Constructors
        public Component()
        {
            this.name = "Component";
            Weight = 30;
            TotalGrade = new ComputedGrade(0,0);
            Assignments = new ObservableCollection<Assignment>();
        }

        public Component(string name, double weight)
        {
            this.name = "Component";
            this.Weight = weight;
            TotalGrade = new ComputedGrade(0, 0);
            Assignments = new ObservableCollection<Assignment>();
        }
        #endregion
    }
}
