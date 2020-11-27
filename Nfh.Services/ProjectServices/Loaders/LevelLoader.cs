﻿using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Converters;
using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Serializers;
using System.IO;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal class LevelLoader : ILevelLoader
    {
        private readonly ILevelDataLoader levelDataLoader;
        private readonly ILevelMetaLoader levelMetaLoader;
        private readonly ILevelDataUnifier levelDataUnifier;
        private readonly IConverter converter;

        public LevelLoader(
            ILevelDataLoader levelDataLoader,
            ILevelMetaLoader levelMetaLoader,
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
