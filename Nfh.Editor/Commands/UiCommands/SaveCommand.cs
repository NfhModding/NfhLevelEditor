using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class SaveCommand : UiCommand
    {
        public override string Name => "Save";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.S, ModifierKeys.Control);

        public SaveCommand()
            : base(p => App.Save(), p => Services.UndoRedo.HasUnsavedChanges)
        {
        }
    }
}
