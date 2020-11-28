using Nfh.Editor.ViewModels;
using System;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class SaveCommand : UiCommand
    {
        public override string Name => "Save";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.S, ModifierKeys.Control);

        public SaveCommand()
            : base(p => Save(p), p => CanSave(p))
        {
        }

        private static void Save(object parameter)
        {
            if (!(parameter is ITopLevelViewModel vm))
            {
                throw new ArgumentException("The parameter of a save command must be a top level VM!", nameof(parameter));
            }
            vm.SaveChanges();
        }

        private static bool CanSave(object parameter)
        {
            if (parameter == null) return false;
            if (!(parameter is ITopLevelViewModel vm))
            {
                throw new ArgumentException("The parameter of a save command must be a top level VM!", nameof(parameter));
            }
            return vm.UndoRedo.HasUnsavedChanges;
        }
    }
}
