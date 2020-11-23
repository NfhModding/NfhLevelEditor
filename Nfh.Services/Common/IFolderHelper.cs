using System.IO;

namespace Nfh.Services.Common
{
    internal interface IFolderHelper
    {
        public bool IsValidSourcePath(string gameSourcePath);
        public DirectoryInfo GetGamesDataFolder(string gameSourcePath);
    }
}
