using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class NewCommand : UiCommand
    {
        public override string Name => "New";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.N, ModifierKeys.Control);

        public NewCommand()
            : base(p => { /* TODO */ }, p => true)
        {
        }
    }
}
