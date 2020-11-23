using Nfh.Services.ProjectServices.Xml.Models;
using System.IO;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal interface ILevelDataLoader
    {
        XmlLevelData LoadGenericData(DirectoryInfo gamedataFolder);
        XmlLevelData LoadLevelSpecificData(DirectoryInfo gamedataFolder, string levelId);
    }
}
