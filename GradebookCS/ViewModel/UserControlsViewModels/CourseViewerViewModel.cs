using GradebookCS.Common;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace GradebookCS.ViewModel.UserControlsViewModels
{
    public class CourseViewerViewModel :BaseINPC
    {
        #region Attributes
        private ComponentViewerViewModel selectedComponentViewerViewModel;
        #endregion

        #region Properties
        #region Command Properties
        /// <summary>
        /// Command to edit the course info like name and letter score ranges
        /// </summary>
        public RelayCommand EditCourseInfoCommand { get; private set; }

        /// <summary>
        /// Command to delele this course
        /// </summary>
        public RelayCommand DeleteCourseCommand { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand AddNewComponentCommand { get; private set; }
        #endregion

        #region Other Properties
        public Course Course { get; private set; }

        public ComponentViewerViewModel SelectedComponentViewerViewModel
        {
            get { return selectedComponentViewerViewModel; }
            set
            {
                if(value != selectedComponentViewerViewModel)
                {
                    selectedComponentViewerViewModel = value;
                    onPropertyChanged();
                }
            }
        }

        public ObservableCollection<ComponentViewerViewModel> ComponentViewerViewModels { get; private set; }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes an instance of this class
        /// </summary>
        public CourseViewerViewModel()
        {
            Course = new Course();
            EditCourseInfoCommand = new RelayCommand(async() => await EditCourseInfo(), () => true);
            DeleteCourseCommand = new RelayCommand(() => Delete(), () => true);
            AddNewComponentCommand = new RelayCommand(()=> AddNewComponent(), ()=> true);
            ComponentViewerViewModels = new ObservableCollection<ComponentViewerViewModel>();
            PopulateModels();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shows a dialog box to enable the user to edit some info about the <see cref="Course"/>
        /// </summary>
        public async Task<ContentDialogResult> EditCourseInfo()
        {
            //Backs up the data before its changed in case of need to revert back
            string name = Course.Name;
            double aLow = Course.Grade.ARangeLowEnd;
            double aHigh = Course.Grade.ARangeHighEnd;
            double bLow = Course.Grade.BRangeLowEnd;
            double bHigh = Course.Grade.BRangeHighEnd;
            double cLow = Course.Grade.CRangeLowEnd;
            double cHigh = Course.Grade.CRangeHighEnd;
            double nrLow = Course.Grade.NRRangeLowEnd;
            double nrHigh = Course.Grade.NRRangeHighEnd;

            CourseInfoDialogViewModel dialogViewModel = new CourseInfoDialogViewModel(Course);
            var result = await dialogViewModel.GetDialogResult();
            if (result == ContentDialogResult.Secondary)
            {
                Course.Name = name;
                Course.Grade.ARangeLowEnd = aLow;
                Course.Grade.ARangeHighEnd = aHigh;
                Course.Grade.BRangeLowEnd = bLow;
                Course.Grade.BRangeHighEnd = bHigh;
                Course.Grade.CRangeLowEnd = cLow;
                Course.Grade.CRangeHighEnd = cHigh;
                Course.Grade.NRRangeLowEnd = nrLow;
                Course.Grade.NRRangeHighEnd = nrHigh;
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

        public void AddNewComponent()
        {
            ComponentViewerViewModel newComponentModel = new ComponentViewerViewModel();
            ComponentViewerViewModels.Add(newComponentModel);
            newComponentModel.IsInEditMode = true;
        }
        #endregion

        #region to be deleted
        public CourseViewerViewModel(Course course)
        {
            this.Course = course;
            EditCourseInfoCommand = new RelayCommand(async () => await EditCourseInfo(), () => true);
            DeleteCourseCommand = new RelayCommand(() => Delete(), () => true);
            AddNewComponentCommand = new RelayCommand(() => AddNewComponent(), () => true);
            ComponentViewerViewModels = new ObservableCollection<ComponentViewerViewModel>();
            PopulateModels();
        }

        private void PopulateModels()
        {
            ComponentViewerViewModel cvvm1 = new ComponentViewerViewModel();
            ComponentViewerViewModel cvvm2 = new ComponentViewerViewModel();
            ComponentViewerViewModels.Add(cvvm1);
            ComponentViewerViewModels.Add(cvvm2);
        }

        
        #endregion


    }
}
