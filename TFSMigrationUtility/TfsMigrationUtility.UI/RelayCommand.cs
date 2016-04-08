using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TfsMigrationUtility.UI
{
    public class RelayCommand : ICommand
    {
        private Action<object> ExecuteAction { get; set; }
        private Predicate<object> CanExecutePredictate { get; set; }


        public RelayCommand(Action<object> exec) : this(exec, null)
        {
        }
        public RelayCommand(Action<object> exec, Predicate<object> predict)
        {
            ExecuteAction = exec;
            CanExecutePredictate = predict;
        }
        public bool CanExecute(object parameter)
        {
            if (CanExecutePredictate != null)
                return CanExecutePredictate(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (ExecuteAction != null)
                ExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
