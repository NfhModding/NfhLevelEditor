using Nfh.Dal.Xml.Models;
using System.IO;
using System;

namespace Nfh.Dal.Repositories
{
    internal interface ILevelDataRepository
    {
        XmlLevelData LoadGenericData(DirectoryInfo gamedataFolder);
        XmlLevelData LoadLevelSpecificData(DirectoryInfo gamedataFolder, string levelId);

        void SaveLevelSpecificData(DirectoryInfo gamedataFolder, string levelId, XmlLevelData levelData);
    }
}
