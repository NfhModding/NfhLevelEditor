using Microsoft.Win32;
using Nfh.Editor.Dialogs;
using Nfh.Editor.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Nfh.Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!FindGameInstallation()) return;
            BackupIfNeeded();
        }

        private static bool FindGameInstallation()
        {
            // Find game installations
            var gameInstalls = Services.GameLocator.GetGameLocations().ToList();
            // Choose one
            if (gameInstalls.Count == 0)
            {
                // Need manual selection
                MessageBox.Show(
                    "There's no game installation found on your computer. Please locate it manually.",
                    "Game installation",
                    MessageBoxButton.OK);
                var dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    Current.Shutdown();
                    return false;
                }
                Services.GamePath = dialog.SelectedPath;
            }
            else if (gameInstalls.Count == 1)
            {
                // Single installation, use that
                Services.GamePath = gameInstalls[0];
            }
            else
            {
                // Multiple installations, ask user which one to use
                var dialog = new SelectInstallationDialog(gameInstalls);
                if ((dialog.ShowDialog() ?? false) == false)
                {
                    Current.Shutdown();
                    return false;
                }
                Services.GamePath = dialog.ChosenInstallation;
            }
            // Handle error gracefully, if for some reason it's still null
            if (Services.GamePath == null)
            {
                MessageBox.Show(
                    "Could not determine game installation!",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Current.Shutdown();
                return false;
            }
            return true;
        }

        private static void BackupIfNeeded()
        {
            if (!Services.Backup.BackupExists)
            {
                if (MessageBox.Show(
                    "There's no backup from your original game. Would you like create one now?",
                    "Backup",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Services.Backup.BackupGameData(Services.GamePath);
                }
            }
        }

        public static void Save()
        {
            if (MetaViewModel.Current != null && MetaViewModel.Current.SeasonPack != null)
            {
                Services.Project.SaveSeasonPack(
                    MetaViewModel.Current.SeasonPack.SeasonPack,
                    MetaViewModel.Current.SeasonPack.Path);
            }
            // TODO: Save level if needed
            Services.UndoRedo.Save();
        }

        public static MessageBoxResult SaveIfHasChanges()
        {
            if (!Services.UndoRedo.HasUnsavedChanges) return MessageBoxResult.Yes;
            // Prompt if we want to save
            var result = MessageBox.Show(
                "Would you like to save the current changes?",
                "Save",
                MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) Save();
            return result;
        }

        public static void PatchGame()
        {
            Save();
            if (MetaViewModel.Current != null && MetaViewModel.Current.SeasonPack != null)
            {
                Services.Project.PatchGame(
                    MetaViewModel.Current.SeasonPack.Path,
                    Services.GamePath);
            }
        }
    }
}
