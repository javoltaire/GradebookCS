using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradebookCS.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        #region Attributes
        /// <summary>
        /// The command to execute
        /// </summary>
        private Action execute;

        /// <summary>
        /// Indicates whether the command can be executed
        /// </summary>
        private Func<bool> canExecute;

        /// <summary>
        /// Event handler for when the CanExecute has changed
        /// </summary>
        public event EventHandler CanExecuteChanged;
        #endregion

        #region Constructors
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute();
            else
                return true;
        }

        public void Execute(object parameter)
        {
            execute();
        }

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }
        #endregion
    }
}
