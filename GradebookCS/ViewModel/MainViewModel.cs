using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradebookCS.DataBase;
using Windows.UI.Xaml.Controls;
using GradebookCS.View;
using System.Collections.ObjectModel;
using GradebookCS.Model;
using GradebookCS.ViewModel.Commands;
using GradebookCS.Common;

namespace GradebookCS.ViewModel
{
    public class MainViewModel : BaseINPC
    {
        #region Attributes
        /// <summary>
        /// The current Page type to be shown
        /// </summary>
        private Type currentPageType;

        /// <summary>
        /// The selected Course to view details of
        /// </summary>
        private CourseViewModel selectedCourseViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the page type being shown
        /// </summary>
        public Type CurrentPageType
        {
            get { return currentPageType; }
            set
            {
                if (value != currentPageType)
                {
                    currentPageType = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or Sets the selected Course view model object
        /// </summary>
        public CourseViewModel SelectedCourseViewModel
        {
            get { return selectedCourseViewModel; }
            set
            {
                if (value != selectedCourseViewModel)
                {
                    selectedCourseViewModel = value;
                    onPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the CourseListPageViewModel
        /// </summary>
        public CourseListViewModel CourseListViewModel { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of the MainPageViewModel class
        /// </summary>
        public MainViewModel()
        {
            CourseListViewModel = new CourseListViewModel(this);
            CurrentPageType = typeof(CourseListPage);
        }
        #endregion

    }
}
