using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Input;

namespace GradebookCS.View.CustomControls
{
    public class NumericTextbox : AutoSelectOnFocusTextBox
    {
        public NumericTextbox() : base()
        {
            this.KeyDown += NumericTextbox_KeyDown;
            this.Paste += NumericTextbox_Paste;
        }

        private void NumericTextbox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            char keyPressed = char.Parse(e.Key.ToString());
            if (!char.IsControl(keyPressed) && !char.IsDigit(keyPressed) && (keyPressed != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((keyPressed == '.') && (this.Text.Contains(".")))
            {
                e.Handled = true;
                
            }
        }

        private void NumericTextbox_Paste(object sender, Windows.UI.Xaml.Controls.TextControlPasteEventArgs e)
        {
            throw new NotImplementedException();
        }

        

    }
}
