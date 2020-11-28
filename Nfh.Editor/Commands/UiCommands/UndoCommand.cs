using Nfh.Editor.ViewModels;
using System;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class UndoCommand : UiCommand
    {
        public override string Name => "Undo";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.Z, ModifierKeys.Control);

        public UndoCommand() 
            : base(p => Undo(p), p => CanUndo(p))
        {
        }

        private static void Undo(object parameter)
        {
            if (!(parameter is ITopLevelViewModel vm))
            {
                throw new ArgumentException("The parameter of an undo command must be a top level VM!", nameof(parameter));
            }
            vm.UndoRedo.Undo();
        }

        private static bool CanUndo(object parameter)
        {
            if (parameter == null) return false;
            if (!(parameter is ITopLevelViewModel vm))
            {
                throw new ArgumentException("The parameter of an undo command must be a top level VM!", nameof(parameter));
            }
            return vm.UndoRedo.CanUndo;
        }
    }
}
