using System;
using System.Windows.Input;

namespace Ptarmigan.Utils
{
    public interface INamedCommand : INamed, ICommand
    {
        void NotifyCanExecuteChanged();
    }

    public interface INamedCommand<T> : INamedCommand
    {
        bool CanExecute(T parameter);
        void Execute(T parameter);
    }

    public class NamedCommand : INamedCommand
    {
        public Action ExecuteAction { get; }
        public string Name { get; }
        public Func<bool> CanExecuteFunc { get; }

        public NamedCommand(string name, Action execute, Func<bool> canExecute)
            => (Name, ExecuteAction, CanExecuteFunc) = (name, execute, canExecute);

        public bool CanExecute(object parameter)
            => CanExecuteFunc?.Invoke() ?? true;

        public void Execute(object parameter)
            => ExecuteAction();

        public void NotifyCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler CanExecuteChanged;
    }

    public class NamedCommand<T> : INamedCommand<T>
    {
        public Action<T> ExecuteAction { get; }
        public string Name { get; }
        public Func<T, bool> CanExecuteFunc { get; }

        public NamedCommand(string name, Action<T> execute, Func<T, bool> canExecute)
            => (Name, ExecuteAction, CanExecuteFunc) = (name, execute, canExecute);

        public bool CanExecute(T parameter)
            => CanExecuteFunc(parameter);

        public bool CanExecute(object parameter)
            => CanExecute((T)parameter);

        public void Execute(object parameter)
            => Execute((T)parameter);

        public void Execute(T parameter)
            => ExecuteAction(parameter);

        public void NotifyCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler CanExecuteChanged;
    }
}