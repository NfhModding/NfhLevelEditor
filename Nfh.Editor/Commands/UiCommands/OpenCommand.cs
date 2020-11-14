using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class OpenCommand : UiCommand
    {
        public override string Name => "Open";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.O, ModifierKeys.Control);

        public OpenCommand()
            : base(p => { /* TODO */ }, p => true)
        {
        }
    }
}
