using Nfh.Dal.Xml.Converters;
using Nfh.Dal.Xml.Models;
using Nfh.Domain.Models.InGame;
using System.IO;

namespace Nfh.Dal.Repositories
{
    internal class LevelRepository : ILevelRepostory
    {
        private readonly ILevelDataRepository levelDataLoader;
        private readonly ILevelMetaRepository levelMetaLoader;
        private readonly ILevelDataUnifier levelDataUnifier;
        private readonly IConverter converter;

        public LevelRepository(
            ILevelDataRepository levelDataLoader,
            ILevelMetaRepository levelMetaLoader,
            ILevelDataUnifier levelDataUnifier,
            IConverter converter)
        {
            this.levelDataLoader = levelDataLoader;
            this.levelMetaLoader = levelMetaLoader;
            this.levelDataUnifier = levelDataUnifier;
            this.converter = converter;
        }

        public Level Load(DirectoryInfo gamedataFolder, string levelId)
        {
            var levelData = levelDataUnifier.UnifyWithGeneric(
                generic: levelDataLoader.LoadGenericData(gamedataFolder),
                level: levelDataLoader.LoadLevelSpecificData(gamedataFolder, levelId));

            var level = converter.Convert<XmlLevelData, Level>(levelData);
            level.Meta = levelMetaLoader.Load(gamedataFolder, levelId);

            return level;
        }

        public void Save(DirectoryInfo gamedataFolder, Level level)
        {
            var allLevelData = converter.Convert<Level, XmlLevelData>(level);
            var meta = level.Meta; // Meta is not modified in the level's editor (yet)

            var generic = levelDataLoader.LoadGenericData(gamedataFolder);
            var levelSpecific = levelDataUnifier.SeperateFromGeneric(generic, allLevelData);
            levelDataLoader.SaveLevelSpecificData(gamedataFolder, level.Id, levelSpecific);
        }
    }
}
