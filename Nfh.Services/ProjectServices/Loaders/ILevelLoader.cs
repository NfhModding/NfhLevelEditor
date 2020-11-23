using Nfh.Domain.Models.InGame;
using System.IO;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal interface ILevelLoader
    {
        Level Load(DirectoryInfo gamedataFolder, string levelId);
        void Save(DirectoryInfo gamedataFolder, Level level);
    }
}
