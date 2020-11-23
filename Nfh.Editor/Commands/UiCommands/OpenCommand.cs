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
            if (App.SaveIfHasChanges() == MessageBoxResult.Cancel) return;
            // Browse the folder
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            // Load the project from there
            Services.UndoRedo.Reset();
            Services.ProjectPath = dialog.SelectedPath;
            metaViewModel.SeasonPack = new SeasonPackViewModel(
                dialog.SelectedPath,
                Services.Project.LoadSeasonPack(dialog.SelectedPath));
        }
    }
}
