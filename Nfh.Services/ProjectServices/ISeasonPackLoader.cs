using Nfh.Domain.Models.Meta;
using System.IO;

namespace Nfh.Services.ProjectServices
{
    internal interface ISeasonPackLoader
    {
        SeasonPack Load(DirectoryInfo gamedataFolder);
        void Save(SeasonPack seasonPack, DirectoryInfo gamedataFolder);
    }
}
