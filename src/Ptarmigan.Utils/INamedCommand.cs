using System.Windows.Input;

namespace Ptarmigan.Utils
{
    public interface INamedCommand : INamed, ICommand
    {
        string Name { get; }
    }

    public interface INamedCommand<T> : INamedCommand
    {
        bool CanExecute(T parameter);
        void Execute(T parameter);
    }

    // TODO: this is a service backed command. It needs to move 
}