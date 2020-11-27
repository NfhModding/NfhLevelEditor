using System.IO;

namespace Nfh.Services.Common
{
    internal interface IFolderService
    {
        public bool IsValidSourcePath(string gameSourcePath);
        public DirectoryInfo GetGamesDataFolder(string gameSourcePath);
    }
}
