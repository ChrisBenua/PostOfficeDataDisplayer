using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PostOfficesDataDisplayer.Utils
{
    /// <summary>
    /// Relay command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to be executed.
        /// </summary>
        private Action<object> execute;

        /// <summary>
        /// Can Execute Predicte
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Occurs when can execute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.Utils.RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">Execute.</param>
        /// <param name="canExecute">Can execute.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Cans the execute.
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        /// Execute the specified parameter.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
