using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Converters;
using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal class LevelLoader : ILevelLoader
    {
        private readonly ILevelDataLoader levelDataLoader;
        //private readonly ILevelMetaLoader levelMetaLoader;
        private readonly ILevelDataUnifier levelDataUnifier;
        private readonly IConverter converter;

        public LevelLoader(
            ILevelDataLoader levelDataLoader,
            //ILevelMetaLoader levelMetaLoader,
            ILevelDataUnifier levelDataUnifier,
            IConverter converter)
        {
            this.levelDataLoader = levelDataLoader;
            // this.levelMetaLoader = levelMetaLoader;
            this.levelDataUnifier = levelDataUnifier;
            this.converter = converter;
        }

        public Level Load(DirectoryInfo gamedataFolder, string levelId)
        {
            var levelData = levelDataUnifier.UnifyWithGeneric(
                generic: levelDataLoader.LoadGenericData(gamedataFolder),
                level: levelDataLoader.LoadLevelSpecificData(gamedataFolder, levelId));

            var level = converter.Convert<XmlLevelData, Level>(levelData);
            // level.Meta = levelMetaLoader.Load(gamedataFolder, levelId);

            return level;
        }

        public void Save(DirectoryInfo gamedataFolder, Level level)
        {
            throw new NotImplementedException();
        }
    }
}
