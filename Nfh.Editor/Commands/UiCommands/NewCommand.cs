using Nfh.Editor.ViewModels;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Nfh.Editor.Commands.UiCommands
{
    public class NewCommand : UiCommand
    {
        public override string Name => "New";
        public override InputGesture Gesture { get; } = new KeyGesture(Key.N, ModifierKeys.Control);

        public NewCommand()
            : base(New, p => true)
        {
        }

        private static void New(object? parameter)
        {
            var metaViewModel = parameter as MetaViewModel;
            if (metaViewModel == null) throw new ArgumentException();

            if (metaViewModel.SaveIfHasChanges() == MessageBoxResult.Cancel) return;
            // Browse the folder
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            // Do new project logic
            metaViewModel.NewProject(dialog.SelectedPath);
        }
    }
}
