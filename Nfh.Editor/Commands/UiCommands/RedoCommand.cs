using Mvvm.Framework.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class RedoCommand : UiCommand
    {
        public override string Name => "Redo";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.Y, ModifierKeys.Control);

        public RedoCommand()
            : base(p => Services.UndoRedo.Redo(), p => Services.UndoRedo.CanRedo)
        {
        }
    }
}
