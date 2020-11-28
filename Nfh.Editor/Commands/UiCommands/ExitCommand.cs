using System;
using System.Windows;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class ExitCommand : UiCommand
    {
        public override string? Name => "Exit";

        public override InputGesture? Gesture => new KeyGesture(Key.F4, ModifierKeys.Alt);

        public ExitCommand()
            : base(Exit, p => true)
        {
        }

        private static void Exit(object? parameter)
        {
            if (!(parameter is Window window))
            {
                throw new ArgumentException("Parameter of exit command must be a window!", nameof(parameter));
            }
            window.Close();
        }
    }
}
