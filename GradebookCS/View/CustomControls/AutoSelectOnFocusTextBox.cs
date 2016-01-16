using Windows.UI.Xaml.Controls;

namespace GradebookCS.View.CustomControls
{
    public class AutoSelectOnFocusTextBox : TextBox
    {
        #region Constructors
        /// <summary>
        /// Constructor to initialize this class
        /// </summary>
        public AutoSelectOnFocusTextBox() : base()
        {
            this.GotFocus += AutoSelectOnFocusTextBox_GotFocus;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Selects the text in the TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSelectOnFocusTextBox_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.SelectionStart = 0;
            this.SelectionLength = this.Text.Length;
        }
        #endregion

    }
}
