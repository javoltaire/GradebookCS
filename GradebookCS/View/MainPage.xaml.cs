using GradebookCS.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GradebookCS.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //Shows the back button in the title bar
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            //Adds an event to the back button
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += ContentFrame_BackRequested;
        }

        /// <summary>
        /// Handles what happens the back button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentFrame_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ContentFrame == null)
                return;

            if(ContentFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                ContentFrame.GoBack();
            }
        }
    }
}
