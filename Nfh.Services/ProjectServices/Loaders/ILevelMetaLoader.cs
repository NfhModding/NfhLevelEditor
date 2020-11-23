using Nfh.Domain.Models.Meta;
using System.IO;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal interface ILevelMetaLoader
    {
        // LevelDescription LoadDescription(DirectoryInfo gamedataFolder, string levelId);
        // void SaveLevelDescription(DirectoryInfo gamedataFolder, string levelId, LevelDescription levelDescription);

        LevelMeta Load(DirectoryInfo gamedataFolder, string levelId);
        void Save(DirectoryInfo gamedataFolder, LevelMeta levelMeta);
    }
}
