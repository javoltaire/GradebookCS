using GradebookCS.ViewModel;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class AssignmentUserControl : UserControl, INotifyPropertyChanged
    {
        #region Dependency properties
        /// <summary>
        /// Dependency property to pass the Edit assingnment command to be executed by a button in this UserControl
        /// </summary>
        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(RelayParameterCommand<AssignmentViewModel>), typeof(AssignmentUserControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to pass the Save assingnment command to be executed by a button in this UserControl
        /// </summary>
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register("SaveCommand", typeof(RelayParameterCommand<AssignmentViewModel>), typeof(AssignmentUserControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to pass the delete assingnment command to be executed by a button in this UserControl
        /// </summary>
        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(RelayParameterCommand<AssignmentViewModel>), typeof(AssignmentUserControl), new PropertyMetadata(null));
        #endregion

        #region Properties
        /// <summary>
        /// Gets or Sets the Edit command to be executed
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> EditCommand
        {
            get { return (RelayParameterCommand<AssignmentViewModel>)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        /// <summary>
        /// Gets or Sets the Save command to be executed
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> SaveCommand
        {
            get { return (RelayParameterCommand<AssignmentViewModel>)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        /// <summary>
        /// Gets or Sets the Delete command to be executed
        /// </summary>
        public RelayParameterCommand<AssignmentViewModel> DeleteCommand
        {
            get { return (RelayParameterCommand<AssignmentViewModel>)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public AssignmentUserControl()
        {
            this.InitializeComponent();
            this.DoubleTapped += AssignmentUserControl_DoubleTapped;
            this.KeyDown += AssignmentUserControl_KeyDown;
            //this.PointerEntered += AssignmentUserControl_PointerEntered;
            //this.PointerExited += AssignmentUserControl_PointerExited;
            //this.DataContextChanged += AssignmentUserControl_DataContextChanged;
            //GoToEditMode();
        }
 
        #endregion

        #region Event Listener Methods
        /// <summary>
        /// Event listener for when the user clicks the Edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Tapped(object sender, RoutedEventArgs e)
        {
            EditCommand.Execute(this.DataContext);
        }

        /// <summary>
        /// Event listener for when the user clicks the Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Tapped(object sender, RoutedEventArgs e)
        {
            SaveCommand.Execute(this.DataContext);
        }

        /// <summary>
        /// Event Listener for when the user clicks the delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Tapped(object sender, RoutedEventArgs e)
        {
            DeleteCommand.Execute(this.DataContext);
        }

        /// <summary>
        /// Event listener for when the user double clicks this control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignmentUserControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            EditCommand.Execute(this.DataContext);
        }

        /// <summary>
        /// Event listener for when a key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignmentUserControl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && ((AssignmentViewModel)this.DataContext).IsInEditMode && (HasFocus(NameTextBox) || HasFocus(ScoreTextBox) || HasFocus(MaxScoreTextBox)))
            {
                e.Handled = true;
                SaveCommand.Execute(this.DataContext);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Determines whether a TextBox has focus
        /// </summary>
        /// <param name="tb">The TextBox to check</param>
        /// <returns>A boolean indicating whether the given TextBox has focus</returns>
        private bool HasFocus(TextBox tb)
        {
            return tb.FocusState == FocusState.Keyboard || tb.FocusState == FocusState.Programmatic
                || tb.FocusState == FocusState.Pointer;
        }
        #endregion

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Will notify which property is changed
        /// </summary>
        /// <param name="propertyName">The property changing</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }



        /// <summary>
        /// Gets the Casted Data Context
        /// </summary>
        public AssignmentViewModel AssignmentViewModel { get { return (AssignmentViewModel)DataContext; } }
















        private void AssignmentUserControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            OnPropertyChanged("AssignmentViewModel");
            //AssignmentViewModel context = (AssignmentViewModel)sender.DataContext;
            //if (context != null)
            //    context.PropertyChanged += AssignmentViewModel_PropertyChanged;
        }

        private void AssignmentViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsInEditMode"))
            {
                if (((AssignmentViewModel)sender).IsInEditMode)
                {
                    GoToEditMode();
                    NameTextBox.Focus(FocusState.Pointer);
                }
                else if (!((AssignmentViewModel)sender).IsInEditMode)
                {
                    GoToNormalMode();
                }
            }
                
        }

        private void GoToEditMode()
        {
            NameTextBlock.Visibility = Visibility.Collapsed;
            ScoreTextBlock.Visibility = Visibility.Collapsed;
            MaxScoreTextBlock.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Collapsed;

            NameTextBox.Visibility = Visibility.Visible;
            ScoreTextBox.Visibility = Visibility.Visible;
            MaxScoreTextBox.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void GoToNormalMode()
        {
            NameTextBlock.Visibility = Visibility.Visible;
            ScoreTextBlock.Visibility = Visibility.Visible;
            MaxScoreTextBlock.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;

            NameTextBox.Visibility = Visibility.Collapsed;
            ScoreTextBox.Visibility = Visibility.Collapsed;
            MaxScoreTextBox.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
        }

        private void AssignmentUserControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AssignmentViewModel context = (AssignmentViewModel)((FrameworkElement)sender).DataContext;
            if (!context.IsInEditMode)
            {
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
                
        }

        private void AssignmentUserControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AssignmentViewModel context = (AssignmentViewModel)((FrameworkElement)sender).DataContext;
            if (!context.IsInEditMode)
            {
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
            
        }

    }
}
