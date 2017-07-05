using GradebookCS.ViewModel;
using GradebookCS.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ComponentUserControl : UserControl
    {
        #region Dependency Properties
        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(RelayParameterCommand<ComponentViewModel>), typeof(ComponentUserControl), new PropertyMetadata(null));
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register("SaveCommand", typeof(RelayParameterCommand<ComponentViewModel>), typeof(ComponentUserControl), new PropertyMetadata(null));
        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(RelayParameterCommand<ComponentViewModel>), typeof(ComponentUserControl), new PropertyMetadata(null));
        #endregion

        #region Properties
        public RelayParameterCommand<ComponentViewModel> EditCommand
        {
            get { return (RelayParameterCommand<ComponentViewModel>)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        public RelayParameterCommand<ComponentViewModel> SaveCommand
        {
            get { return (RelayParameterCommand<ComponentViewModel>)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public RelayParameterCommand<ComponentViewModel> DeleteCommand
        {
            get { return (RelayParameterCommand<ComponentViewModel>)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public ComponentUserControl()
        {
            this.InitializeComponent();
            this.DoubleTapped += ComponentUserControl_DoubleTapped;
            this.KeyDown += ComponentUserControl_KeyDown;
        }
        #endregion

        #region Event Listener Methods
        private void DeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DeleteCommand.Execute(this.DataContext);
        }

        private void EditButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EditCommand.Execute(this.DataContext);
        }

        private void SaveButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SaveCommand.Execute(this.DataContext);
        }

        private void ComponentUserControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            EditCommand.Execute(this.DataContext);
        }

        /// <summary>
        /// Event listener for when a key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComponentUserControl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && ((ComponentViewModel)this.DataContext).IsInEditMode && (HasFocus(NameTextBox) || HasFocus(WeightTextBox)))
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


    }
}
