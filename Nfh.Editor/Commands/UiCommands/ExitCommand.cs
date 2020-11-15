using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var window = parameter as Window;
            if (window == null)
            {
                throw new ArgumentException("Wrong command parameter type!", nameof(parameter));
            }
            window.Close();
        }
    }
}
