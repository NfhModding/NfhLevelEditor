using Nfh.Editor.ViewModels;
using System;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class RedoCommand : UiCommand
    {
        public override string Name => "Redo";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.Y, ModifierKeys.Control);

        public RedoCommand()
            : base(p => Redo(p), p => CanRedo(p))
        {
        }

        private static void Redo(object parameter)
        {
            if (!(parameter is ITopLevelViewModel vm))
            {
                throw new ArgumentException("The parameter of a redo command must be a top level VM!", nameof(parameter));
            }
            vm.UndoRedo.Redo();
        }

        private static bool CanRedo(object parameter)
        {
            if (parameter == null) return false;
            if (!(parameter is ITopLevelViewModel vm))
            {
                throw new ArgumentException("The parameter of a redo command must be a top level VM!", nameof(parameter));
            }
            return vm.UndoRedo.CanRedo;
        }
    }
}
