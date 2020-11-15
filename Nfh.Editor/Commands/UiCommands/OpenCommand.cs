using Nfh.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace Nfh.Editor.Commands.UiCommands
{
    public class OpenCommand : UiCommand
    {
        public override string Name => "Open";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.O, ModifierKeys.Control);

        public OpenCommand()
            : base(Open, p => true)
        {
        }

        private static void Open(object? parameter)
        {
            var metaViewModel = parameter as MetaViewModel;
            if (metaViewModel == null)
            {
                throw new ArgumentException("Wrong command parameter type!", nameof(parameter));
            }
            // TODO: Factor this out? Exit and new also need this
            if (Services.UndoRedo.HasUnsavedChanges)
            {
                // Save unsaved changes
                var result = MessageBox.Show(
                    "Would you like to save the current changes?", 
                    "Save", 
                    MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Cancel) return;
                if (result == MessageBoxResult.Yes)
                {
                    // TODO: Do the saving
                    // Maybe even invoke the save command
                    Services.UndoRedo.Save();
                }
            }
            // Browse the folder
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            // Load the project from there
            metaViewModel.SeasonPack = new SeasonPackViewModel(
                Services.Project.LoadSeasonPack(dialog.SelectedPath));
            Services.UndoRedo.Reset();
        }
    }
}
