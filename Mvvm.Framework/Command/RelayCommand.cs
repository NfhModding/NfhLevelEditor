using System;
using System.Windows.Input;

namespace Mvvm.Framework.Command
{
    /// <summary>
    /// A simple <see cref="ICommand"/> that takes regular C# functions to implement functionality.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private Action<T> execute;
        private Predicate<T>? canExecute;

        /// <summary>
        /// Initializes a new <see cref="RelayCommand{T}"/>.
        /// </summary>
        /// <param name="execute">The function that gets executed on <see cref="Execute(object)"/>.</param>
        /// <param name="canExecute">The function that determines the outcome of <see cref="CanExecute(object)"/>.
        /// Defaults to true, if null.</param>
        public RelayCommand(Action<T> execute, Predicate<T>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => canExecute?.Invoke((T)parameter) ?? true;
        public void Execute(object? parameter) => execute((T)parameter);
    }
}
