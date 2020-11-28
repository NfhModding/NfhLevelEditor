using Nfh.Dal.Xml.Converters;
using Nfh.Dal.Xml.Models.Briefing;
using Nfh.Dal.Xml.Models.LevelData;
using Nfh.Dal.Xml.Serializers;
using Nfh.Domain.Models.Meta;
using System.IO;
using System.Linq;

namespace Nfh.Dal.Repositories
{
    internal class LevelMetaLoader : ILevelMetaRepository
    {
        private readonly ISerializer serializer;
        private readonly IConverter converter;

        public LevelMetaLoader(ISerializer serializer, IConverter converter)
        {
            this.serializer = serializer;
            this.converter = converter;
        }

        public LevelMeta Load(DirectoryInfo gamedataFolder, string levelId)
        {
            var levelDataFile = new FileInfo(Path.Combine(gamedataFolder.FullName, "leveldata.xml"));
            var levelData = serializer.DeserializeFromFile<XmlLevelDataRoot>(levelDataFile);

            var set = levelData.Sets
                .SelectMany(s => s.Levels)
                .Where(l => l.Name == levelId)
                .FirstOrDefault();

            if (set is null)
                throw new($"Level with {levelId} is not found");

            return new LevelMeta(set.Name)
            {
                Description = loadLevelDescription(gamedataFolder, set.Name),
                MinPercent = set.MinQuota,
                TrickCount = set.Reachable,
                Unlocked = set.State.IsUnlocked,
                TimeLimit = set.Time,
            };
        }

        public void Save(DirectoryInfo gamedataFolder, LevelMeta levelMeta)
        {
            throw new System.NotImplementedException();
        }

        private LevelDescription loadLevelDescription(DirectoryInfo gamedataFolder, string levelId)
        {
            var file = new FileInfo(Path.Combine(gamedataFolder.FullName, "dialogs", "briefing", $"{levelId}.xml"));
            if (!file.Exists)
                throw new();

            var levelBriefing = serializer.DeserializeFromFile<XmlBriefingRoot>(file);
            return converter.Convert<XmlBriefingRoot, LevelDescription>(levelBriefing);
        }
    }
}
