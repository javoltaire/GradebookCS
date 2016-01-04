using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel.UserControlsViewModels
{
    public class CourseViewModel
    {
        #region Attributes
        private Course course;
        #endregion

        #region Properties
        public String Name
        {
            get { return course.Name; }
            set { course.Name = value; }
        }

        public String Letter { get { return course.Grade.Letter; } }
        public String Score { get { return course.Grade.Score + " / " + course.Grade.MaximumScore; } }
        public EditCourseInfoCommand EditCourseInfoCommand { get; private set; }
        public DeleteCourseCommand DeleteCourseCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes an instance of this class
        /// </summary>
        public CourseViewModel()
        {
            course = new Course();
            EditCourseInfoCommand = new EditCourseInfoCommand(this);
            DeleteCourseCommand = new DeleteCourseCommand(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shows a dialog box to enable the user to edit some info about the <see cref="Course"/>
        /// </summary>
        public async Task<ContentDialogResult> EditCourseInfo()
        {
            //Backs up the data before its changed in case of need to revert back
            string name = course.Name;
            double aLow = course.Grade.ARangeLowEnd;
            double aHigh = course.Grade.ARangeHighEnd;
            double bLow = course.Grade.BRangeLowEnd;
            double bHigh = course.Grade.BRangeHighEnd;
            double cLow = course.Grade.CRangeLowEnd;
            double cHigh = course.Grade.CRangeHighEnd;
            double nrLow = course.Grade.NRRangeLowEnd;
            double nrHigh = course.Grade.NRRangeHighEnd;

            CourseInfoDialogViewModel dialogViewModel = new CourseInfoDialogViewModel(course);
            var result = await dialogViewModel.GetDialogResult();
            if (result == ContentDialogResult.Secondary)
            {
                course.Name = name;
                course.Grade.ARangeLowEnd = aLow;
                course.Grade.ARangeHighEnd = aHigh;
                course.Grade.BRangeLowEnd = bLow;
                course.Grade.BRangeHighEnd = bHigh;
                course.Grade.CRangeLowEnd = cLow;
                course.Grade.CRangeHighEnd = cHigh;
                course.Grade.NRRangeLowEnd = nrLow;
                course.Grade.NRRangeHighEnd = nrHigh;
            }
            return result;
        }

        /// <summary>
        /// Deletes this courseviewmodel from its parent list
        /// </summary>
        public async void Delete()
        {
            ContentDialog deleteDialog = new ContentDialog();
            deleteDialog.Title = "Are you Sure you want to delete this course?";
            deleteDialog.PrimaryButtonText = "Yes";
            deleteDialog.SecondaryButtonText = "No";
            var result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                CourseListPageViewModel.CourseViewModels.Remove(this);
        }
        #endregion
    }
}
