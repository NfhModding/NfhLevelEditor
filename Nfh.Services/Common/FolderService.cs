using System;
using System.IO;

namespace Nfh.Services.Common
{
    internal class FolderService : IFolderService
    {
        public DirectoryInfo GetGamesDataFolder(string gameSourcePath)
        {
            if (!IsValidSourcePath(gameSourcePath))
                throw new Exception($"{gameSourcePath} folder does not exists");

            var dataFolder = new DirectoryInfo(Path.Combine(gameSourcePath, "data"));
            if (!dataFolder.Exists)
                throw new Exception($"Could not find the game's \"/data\" folder!");

            return dataFolder;
        }

        public bool IsValidSourcePath(string gameSourcePath)
        {
            return Directory.Exists(gameSourcePath);
        }
    }
}
