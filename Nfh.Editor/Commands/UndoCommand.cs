using Mvvm.Framework.Command;
using Mvvm.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands
{
    public class UndoCommand : UiCommand
    {
        public override string Name => "Undo";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.Z, ModifierKeys.Control);

        public UndoCommand() 
            : base(p => Services.UndoRedo.Undo(), p => Services.UndoRedo.CanUndo)
        {
        }
    }
}
