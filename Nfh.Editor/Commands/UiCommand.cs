using Mvvm.Framework.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands
{
    public abstract class UiCommand : RelayCommand<object>
    {
        public abstract string? Name { get; }
        public abstract InputGesture? Gesture { get; }

        protected UiCommand(Action<object> execute, Predicate<object>? canExecute = null) 
            : base(execute, canExecute)
        {
        }
    }
}
