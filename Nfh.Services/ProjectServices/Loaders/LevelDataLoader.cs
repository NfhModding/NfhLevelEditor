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

        public LevelData LoadGenericData(DirectoryInfo gamedataFolder) => new()
        {
            StringsRoot = loadGeneric<StringsRoot>(gamedataFolder, "strings"),
            GfxDataRoot = loadGeneric<GfxDataRoot>(gamedataFolder, "gfxdata"),
            AnimsRoot = loadGeneric<AnimsRoot>(gamedataFolder, "anims"),
            ObjectsRoot = loadGeneric<ObjectsRoot>(gamedataFolder, "objects"),
        };

        public LevelData LoadLevelSpecificData(DirectoryInfo gamedataFolder, string levelId) => new()
        {
            LevelRoot = loadFromFolder<LevelRoot>(gamedataFolder, levelId, "level"),
            StringsRoot = loadFromFolder<StringsRoot>(gamedataFolder, levelId, "strings"),
            GfxDataRoot = loadFromFolder<GfxDataRoot>(gamedataFolder, levelId, "gfxdata"),
            AnimsRoot = loadFromFolder<AnimsRoot>(gamedataFolder, levelId, "anims"),
            ObjectsRoot = loadFromFolder<ObjectsRoot>(gamedataFolder, levelId, "objects"),
        };

        private T loadFromFolder<T>(DirectoryInfo gamedataFolder, string folderName, string fileName)
            where T : new()
        {
            var levelFile = new FileInfo(Path.Combine(gamedataFolder.FullName, folderName, $"{fileName}.xml"));
            return serializer.DeserializeFromFile<T>(levelFile);
        }

        private T loadGeneric<T>(DirectoryInfo gamedataFolder, string fileName)
            where T : new() => loadFromFolder<T>(gamedataFolder, "generic", fileName);
    }
}
