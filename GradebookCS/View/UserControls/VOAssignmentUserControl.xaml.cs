using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GradebookCS.View.UserControls
{
    public sealed partial class ViewOnlyAssignmentViewer : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty AssignmentNameProperty = DependencyProperty.Register("AssignmentName", typeof(string), typeof(ViewOnlyAssignmentViewer), new PropertyMetadata("N/A"));
        public static readonly DependencyProperty AssignmentScoreProperty = DependencyProperty.Register("AssignmentScore", typeof(double), typeof(ViewOnlyAssignmentViewer), new PropertyMetadata("N/A"));
        public static readonly DependencyProperty AssignmentMaximumScoreProperty = DependencyProperty.Register("AssignmentMaximumScore", typeof(double), typeof(ViewOnlyAssignmentViewer), new PropertyMetadata("N/A"));
        public static readonly DependencyProperty AssignmentPercentProperty = DependencyProperty.Register("AssignmentPercent", typeof(string), typeof(ViewOnlyAssignmentViewer), new PropertyMetadata("N/A"));
        public static readonly DependencyProperty AssignmentLetterProperty = DependencyProperty.Register("AssignmentLetter", typeof(string), typeof(ViewOnlyAssignmentViewer), new PropertyMetadata(""));
        #endregion

        #region Properties
        public string AssignmentName
        {
            get { return (string)GetValue(AssignmentNameProperty); }
            set { SetValue(AssignmentNameProperty, value); }
        }

        public string AssignmentScore
        {
            get { return (string)GetValue(AssignmentScoreProperty); }
            set { SetValue(AssignmentScoreProperty, value); }
        }

        public string AssignmentMaximumScore
        {
            get { return (string)GetValue(AssignmentMaximumScoreProperty); }
            set { SetValue(AssignmentMaximumScoreProperty, value); }
        }

        public string AssignmentPercent
        {
            get { return (string)GetValue(AssignmentPercentProperty); }
            set { SetValue(AssignmentPercentProperty, value); }
        }

        public string AssignmentLetter
        {
            get { return (string)GetValue(AssignmentLetterProperty); }
            set { SetValue(AssignmentLetterProperty, value); }
        }
        #endregion

        public ViewOnlyAssignmentViewer()
        {
            this.InitializeComponent();
        }
    }
}
