using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel.UserControlsViewModels
{
    public class CourseViewModel
    {
        
        public Course Course { get; private set; }
        public EditCourseInfoCommand EditCourseInfoCommand { get; private set; }


        public CourseViewModel()
        {
            Course = new Course();

            EditCourseInfoCommand = new EditCourseInfoCommand(this);
        }

        public async void EditCourseInfo()
        {
            double aLow = Course.Grade.ARangeLowEnd;
            double aHigh = Course.Grade.ARangeHighEnd;
            double bLow = Course.Grade.BRangeLowEnd;
            double bHigh = Course.Grade.BRangeHighEnd;
            double cLow = Course.Grade.CRangeLowEnd;
            double cHigh = Course.Grade.CRangeHighEnd;
            double nrLow = Course.Grade.NRRangeLowEnd;
            double nrHigh = Course.Grade.NRRangeHighEnd;

            CourseInfoDialogViewModel dialogViewModel = new CourseInfoDialogViewModel(Course);
            var result = await dialogViewModel.DialogResult();
            if(result == ContentDialogResult.Secondary)
            {
                Course.Grade.ARangeLowEnd = aLow;
                Course.Grade.ARangeHighEnd = aHigh;
                Course.Grade.BRangeLowEnd = bLow;
                Course.Grade.BRangeHighEnd = bHigh;
                Course.Grade.CRangeLowEnd = cLow;
                Course.Grade.CRangeHighEnd = cHigh;
                Course.Grade.NRRangeLowEnd = nrLow;
                Course.Grade.NRRangeHighEnd = nrHigh;
            }

        }
    }
}
