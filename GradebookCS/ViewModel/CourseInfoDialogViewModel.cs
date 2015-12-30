using GradebookCS.Model;
using GradebookCS.View;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.Enums;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel
{
    public class CourseInfoDialogViewModel
    {
        private Course course;
        CourseInfoDialog infoDialog;

        public bool CanSubmit { get; private set; } = false;

        public CourseInfoDialogViewModel(Course course)
        {
            this.course = course;
            infoDialog = new CourseInfoDialog();
            infoDialog.DataContext = this.course;
        }

        public async Task<ContentDialogResult> DialogResult()
        {
            var result = await infoDialog.ShowAsync();
            return result;
        }

    }
}
