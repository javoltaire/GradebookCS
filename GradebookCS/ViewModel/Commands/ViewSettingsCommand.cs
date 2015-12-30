using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradebookCS.ViewModel.Commands
{
    public class ViewSettingsCommand : ICommand
    {
        private MainPageViewModel mainPageViewModel;
        public ViewSettingsCommand(MainPageViewModel mainPageViewModel)
        {
            this.mainPageViewModel = mainPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return mainPageViewModel.CanShowSettingsPage;
        }

        public void Execute(object parameter)
        {
            mainPageViewModel.ViewSettingsPage();
        }
    }
}
