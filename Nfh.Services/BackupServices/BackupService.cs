using Nfh.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nfh.Services.BackupServices
{
    internal class BackupService : IBackupService
    {
        private readonly IReadOnlyCollection<string> FilesToBackup =
            new List<string> { "gamedata.bnd" };

        private readonly IApplicationWorkFolder applicationWorkFolder;

        public BackupService(IApplicationWorkFolder applicationWorkFolder)
        {
            this.applicationWorkFolder = applicationWorkFolder;
        }

        public void BackupGameData(string sourceGamePath)
        {
            if (!Directory.Exists(sourceGamePath))
                throw new Exception($"{sourceGamePath} folder does not exists");

            copyGamesData(
                from: GetGamesDataFolder(sourceGamePath),
                to: getBackupFolder());
        }

        public void RestoreGameData(string targetGamePath)
        {
            if (!Directory.Exists(targetGamePath))
                throw new Exception($"{targetGamePath} folder does not exists");

            copyGamesData(
                from: getBackupFolder(),
                to: GetGamesDataFolder(targetGamePath));
        }

        private static DirectoryInfo GetGamesDataFolder(string gamePath)
        {
            var dataFolder = new DirectoryInfo(Path.Combine(gamePath, "data"));
            if (!dataFolder.Exists)
                throw new Exception($"Could not find the game's \"/data\" folder!");

            return dataFolder;
        }

        private DirectoryInfo getBackupFolder()
        {
            var backupFolder = new DirectoryInfo(Path.Combine(applicationWorkFolder.Info.FullName, "backup"));
            backupFolder.Create();

            return backupFolder;
        }

        private void copyGamesData(DirectoryInfo from, DirectoryInfo to)
        {
            foreach (var file in from.GetFiles()
                .Where(f => FilesToBackup.Contains(f.Name)))
            {
                file.CopyTo(Path.Combine(to.FullName, file.Name), overwrite: true);
            }
        }
    }
}
