using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;
using Nfh.Services.ProjectServices.Xml.Serializers;
using System.IO;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal class LevelDataLoader : ILevelDataLoader
    {
        private readonly ISerializer serializer;

        public LevelDataLoader(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public XmlLevelData LoadGenericData(DirectoryInfo gamedataFolder) => new()
        {
            StringsRoot = loadGeneric<XmlStringsRoot>(gamedataFolder, "strings"),
            GfxDataRoot = loadGeneric<XmlGfxRoot>(gamedataFolder, "gfxdata"),
            AnimsRoot = loadGeneric<XmlAnimsRoot>(gamedataFolder, "anims"),
            ObjectsRoot = loadGeneric<XmlObjectsRoot>(gamedataFolder, "objects"),
        };

        public XmlLevelData LoadLevelSpecificData(DirectoryInfo gamedataFolder, string levelId) => new()
        {
            LevelRoot = loadFromFolder<XmlLevelRoot>(gamedataFolder, levelId, "level"),
            StringsRoot = loadFromFolder<XmlStringsRoot>(gamedataFolder, levelId, "strings"),
            GfxDataRoot = loadFromFolder<XmlGfxRoot>(gamedataFolder, levelId, "gfxdata"),
            AnimsRoot = loadFromFolder<XmlAnimsRoot>(gamedataFolder, levelId, "anims"),
            ObjectsRoot = loadFromFolder<XmlObjectsRoot>(gamedataFolder, levelId, "objects"),
        };

        public void SaveLevelSpecificData(DirectoryInfo gamedataFolder, string levelId, XmlLevelData levelData)
        {
            saveToFolder(gamedataFolder, levelData.LevelRoot, levelId, "level");
            saveToFolder(gamedataFolder, levelData.StringsRoot, levelId, "strings");
            // saveToFolder(gamedataFolder, levelData.GfxDataRoot, levelId, "gfxdata");
            // saveToFolder(gamedataFolder, levelData.AnimsRoot, levelId, "anims");
            // saveToFolder(gamedataFolder, levelData.ObjectsRoot, levelId, "objects");
        }

        private T loadFromFolder<T>(DirectoryInfo gamedataFolder, string folderName, string fileName)
            where T : new()
        {
            var levelFile = new FileInfo(Path.Combine(gamedataFolder.FullName, folderName, $"{fileName}.xml"));
            return serializer.DeserializeFromFile<T>(levelFile);
        }

        private T loadGeneric<T>(DirectoryInfo gamedataFolder, string fileName)
                where T : new() => 
            loadFromFolder<T>(gamedataFolder, "generic", fileName);

        private void saveToFolder<T>(DirectoryInfo gamedataFolder, T data, string folderName, string fileName)
            where T : notnull
        {
            var levelFile = new FileInfo(Path.Combine(gamedataFolder.FullName, folderName, $"{fileName}.xml")); ;
            serializer.SerializeToFile(data, levelFile);
        }
    }
}
