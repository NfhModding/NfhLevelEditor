using Mvvm.Framework.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public abstract class UiCommand : RelayCommand<object>
    {
        public abstract string? Name { get; }
        public abstract InputGesture? Gesture { get; }
        public string? GestureText => (Gesture as KeyGesture)?.GetDisplayStringForCulture(CultureInfo.CurrentCulture);

        protected UiCommand(Action<object> execute, Predicate<object>? canExecute = null) 
            : base(execute, canExecute)
        {
        }
    }
}
