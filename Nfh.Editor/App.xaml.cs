﻿using Microsoft.Win32;
using Nfh.Editor.Dialogs;
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
                    Shutdown();
                    return;
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
                    Shutdown();
                    return;
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
                Shutdown();
            }
            // Backup
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
    }
}
