using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Models.Meta;
using Nfh.Editor.Commands;
using Nfh.Editor.Commands.ModelCommands;
using Nfh.Editor.Commands.UiCommands;
using Nfh.Editor.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nfh.Editor.ViewModels
{
    public class MetaViewModel : ViewModelBase, ITopLevelViewModel
    {
        // Current meta VM instance
        public static MetaViewModel? Current { get; private set; }

        // Used game installation path
        public string? GamePath { get; private set; }
        // The current project's path
        public string? ProjectPath { get; private set; }

        // Undo-redo stack for metadata
        public IUndoRedoStack UndoRedo { get; } = new UndoRedoStack
        {
            MergeStrategy = new CommandMergeStrategy(),
        };

        // Commands

        public ICommand Exit { get; } = new ExitCommand();

        public ICommand New { get; } = new NewCommand();
        public ICommand Open { get; } = new OpenCommand();
        public ICommand Save { get; } = new SaveCommand();

        public ICommand Undo { get; } = new UndoCommand();
        public ICommand Redo { get; } = new RedoCommand();

        public ICommand Patch { get; } = new PatchCommand();

        // Sub-VM

        private SeasonPackViewModel? seasonPack;
        public SeasonPackViewModel? SeasonPack 
        { 
            get => seasonPack; 
            set
            {
                seasonPack = value;
                NotifyPropertyChanged();
            }
        }

        public MetaViewModel() 
            : base(new ModelChangeNotifier())
        {
            Current = this;
            GamePath = DetermineGamePath();
            BackupIfNeeded();
        }

        public void NewProject(string path)
        {
            if (SaveIfHasChanges() == MessageBoxResult.Cancel) return;

            Services.Project.CreateProject(GamePath, path);
            LoadProject(path);
        }

        public void LoadProject(string path)
        {
            if (SaveIfHasChanges() == MessageBoxResult.Cancel) return;

            ProjectPath = path;
            var seasonPack = new LoadingDialog().Execute(() => Services.Project.LoadSeasonPack(path));
            SeasonPack = new SeasonPackViewModel((SeasonPack)seasonPack);
            UndoRedo.Reset();
        }

        public void PatchGame()
        {
            if (SaveIfHasChanges() == MessageBoxResult.Cancel) return;

            Services.Project.PatchGame(ProjectPath, GamePath);

            MessageBox.Show("Game patched!");
        }

        public MessageBoxResult SaveIfHasChanges()
        {
            if (!UndoRedo.HasUnsavedChanges) return MessageBoxResult.Yes;
            // Prompt if we want to save
            var result = MessageBox.Show(
                "Would you like to save the current changes?",
                "Save",
                MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) SaveChanges();
            return result;
        }

        public void SaveChanges()
        {
            if (SeasonPack == null || ProjectPath == null) return;
            Services.Project.SaveSeasonPack(SeasonPack.SeasonPack, ProjectPath);
            UndoRedo.Save();
        }

        private void BackupIfNeeded()
        {
            if (!Services.Backup.BackupExists)
            {
                if (MessageBox.Show(
                    "There's no backup from your original game. Would you like create one now?",
                    "Backup",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new LoadingDialog().Execute(() =>
                    {
                        Services.Backup.BackupGameData(GamePath);
                        return null;
                    });
                    MessageBox.Show("Successfully backed up game!", "Backup");
                }
            }
        }

        private static string? DetermineGamePath()
        {
            var gameInstalls = Services.GameLocator.GetGameLocations().ToList();
            if (gameInstalls.Count == 0)
            {
                // Need manual selection
                MessageBox.Show(
                    "There's no game installation found on your computer. Please locate it manually.",
                    "Game installation",
                    MessageBoxButton.OK);
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    Application.Current.Shutdown();
                    return null;
                }
                return dialog.SelectedPath;
            }
            else if (gameInstalls.Count == 1)
            {
                // Single installation, use that
                return gameInstalls[0];
            }
            else
            {
                // Multiple installations, ask user which one to use
                var dialog = new SelectInstallationDialog(gameInstalls);
                if ((dialog.ShowDialog() ?? false) == false)
                {
                    Application.Current.Shutdown();
                    return null;
                }
                return dialog.ChosenInstallation;
            }
        }
    }
}
