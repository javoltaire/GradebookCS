﻿using GradebookCS.View.UserControls;
using GradebookCS.ViewModel.UserControlsViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GradebookCS.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CourseDetailsPage : Page
    {
        public CourseDetailsPage()
        {
            this.InitializeComponent();
            AssignmentViewerViewModel a = new AssignmentViewerViewModel();
            assignmetntest.DataContext = a;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComponentViewer item = new ComponentViewer();
            listview.Items.Add(item);
        }
    }
}