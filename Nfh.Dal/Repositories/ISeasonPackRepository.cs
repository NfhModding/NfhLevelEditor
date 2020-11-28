using Nfh.Domain.Models.Meta;
using System.IO;

namespace Nfh.Dal.Repositories
{
    public interface ISeasonPackRepository
    {
        SeasonPack Load(DirectoryInfo gamedataFolder);
        void Save(SeasonPack seasonPack, DirectoryInfo gamedataFolder);
    }
}
