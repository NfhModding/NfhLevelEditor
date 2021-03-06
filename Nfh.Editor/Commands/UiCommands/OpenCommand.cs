﻿using Nfh.Editor.ViewModels;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

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
            if (metaViewModel == null) throw new ArgumentException();

            // Actual logic
            if (metaViewModel.SaveIfHasChanges() == MessageBoxResult.Cancel) return;
            // Browse the folder
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            // Load the project from there
            metaViewModel.LoadProject(dialog.SelectedPath);
        }
    }
}
