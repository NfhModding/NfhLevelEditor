using Nfh.Domain.Models.InGame;
using System.IO;

namespace Nfh.Dal.Repositories
{
    public interface ILevelRepostory
    {
        Level Load(DirectoryInfo gamedataFolder, string levelId);
        void Save(DirectoryInfo gamedataFolder, Level level);
    }
}
