using Nfh.Services.ProjectServices.Xml.Models;
using System.IO;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal interface ILevelDataLoader
    {
        LevelData LoadGenericData(DirectoryInfo gamedataFolder);
        LevelData LoadLevelSpecificData(DirectoryInfo gamedataFolder, string levelId);
    }
}
