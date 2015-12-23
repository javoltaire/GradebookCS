using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Model
{
    public class CoursesManagerSingleton
    {
        #region Properties
        public static CoursesManagerSingleton INSTANCE { get; } = new CoursesManagerSingleton();
        public ObservableCollection<Course> Courses { get; private set; }
        #endregion

        #region Constructors
        private CoursesManagerSingleton()
        {
            this.Courses = new ObservableCollection<Course>();
        }
        #endregion
    }
}
