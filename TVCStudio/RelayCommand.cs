using System;
using System.Windows.Input;

namespace TVCStudio
{
    public class RelayCommand : ICommand
    {
        #region Properties

        private readonly Action<object> m_ExecuteAction;
        private readonly Predicate<object> m_CanExecuteAction;

        #endregion

        public RelayCommand(Action<object> execute)
            : this(execute, _ => true)
        {
        }
        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            m_ExecuteAction = action;
            m_CanExecuteAction = canExecute;
        }

        #region Methods

        public bool CanExecute(object parameter)
        {
            return m_CanExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            m_ExecuteAction(parameter);
        }

        #endregion
    }
}
