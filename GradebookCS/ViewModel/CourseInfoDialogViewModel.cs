using GradebookCS.Model;
using GradebookCS.View;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel
{
    public class CourseInfoDialogViewModel
    {
        #region attributes
        /// <summary>
        /// Course model that will serve as data context for the dialog
        /// </summary>
        private Course course;

        /// <summary>
        /// info dialog to show and edit the course info
        /// </summary>
        private CourseInfoDialog infoDialog;
        #endregion

        #region Constructor
        /// <summary>
        /// Intializes an instance of this class
        /// </summary>
        /// <param name="course">Course for data context</param>
        public CourseInfoDialogViewModel(Course course)
        {
            this.course = course;
            infoDialog = new CourseInfoDialog();
            infoDialog.DataContext = this.course;
        }
        #endregion

        #region Methods
        /// <summary>
        /// shows the dialog and wait for an answer.
        /// </summary>
        /// <returns>The result of the dialog.</returns>
        public async Task<ContentDialogResult> GetDialogResult()
        {
            var result = await infoDialog.ShowAsync();
            return result;
        }
        #endregion
    }
}
