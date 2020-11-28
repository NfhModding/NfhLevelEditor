using Nfh.Domain.Models.Meta;
using System.IO;

namespace Nfh.Dal.Repositories
{
    public interface ILevelMetaRepository
    {
        // LevelDescription LoadDescription(DirectoryInfo gamedataFolder, string levelId);
        // void SaveLevelDescription(DirectoryInfo gamedataFolder, string levelId, LevelDescription levelDescription);

        LevelMeta Load(DirectoryInfo gamedataFolder, string levelId);
        void Save(DirectoryInfo gamedataFolder, LevelMeta levelMeta);
    }
}
