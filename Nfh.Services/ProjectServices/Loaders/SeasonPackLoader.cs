using Nfh.Domain.Models.Meta;
using Nfh.Services.ProjectServices.Loaders;
using Nfh.Services.ProjectServices.Xml.Converters;
using Nfh.Services.ProjectServices.Xml.Models.Briefing;
using Nfh.Services.ProjectServices.Xml.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Serializers;
using System.IO;
using System.Linq;

namespace Nfh.Services.ProjectServices
{
    internal class SeasonPackLoader : ISeasonPackLoader
    {
        private readonly ILevelMetaLoader levelMetaLoader;
        private readonly IConverter converter;
        private readonly ISerializer serializer;

        public SeasonPackLoader(ILevelMetaLoader levelMetaLoader, IConverter converter, ISerializer serializer)
        {
            this.levelMetaLoader = levelMetaLoader;
            this.converter = converter;
            this.serializer = serializer;
        }

        public SeasonPack Load(DirectoryInfo gamedataFolder)
        {
            var levelDataFile = new FileInfo(Path.Combine(gamedataFolder.FullName, "leveldata.xml"));
            var levelData = serializer.DeserializeFromFile<XmlLevelDataRoot>(levelDataFile);

            var seasonpack = new SeasonPack
            {
                Seasons = levelData.Sets
                    .Select((set, i) =>
                    {
                        var season = new Season(set.Name)
                        {
                            Unlocked = set.State.IsUnlocked,
                            Levels = set.Levels
                                .Select((l, i) => (level: levelMetaLoader.Load(gamedataFolder, l.Name), index: i))
                                .ToDictionary(v => v.level.Id, v => v),
                        };

                        return (season, i);
                    })
                    .ToDictionary(v => v.season.Id, v => v),
            };

            foreach (var set in levelData.Sets)
            {
                if (set.NextSet is not null)
                {
                    seasonpack.Seasons[set.Name].Season.Unlocks = seasonpack.Seasons[set.NextSet].Season;
                }
            }

            return seasonpack;
        }

        public void Save(SeasonPack seasonPack, DirectoryInfo gamedataFolder)
        {
            var levelData = new XmlLevelDataRoot
            {
                Sets = seasonPack.Seasons
                .OrderBy(s => s.Value.Index)
                .Select(s => s.Value.Season)
                .Select(s =>
                {
                    var set = new XmlLevelDataSet
                    {
                        Name = s.Id,
                        State = new() { IsUnlocked = s.Unlocked, },
                        NextSet = s.Unlocks?.Id,
                        Levels = s.Levels
                            .OrderBy(l => l.Value.Index)
                            .Select(l => l.Value.Level)
                            .Select(l =>
                            {
                                var level = new XmlLevelDataLevel
                                {
                                    Name = l.Id,
                                    MinQuota = l.MinPercent,
                                    Reachable = l.TrickCount,
                                    State = new() { IsUnlocked = l.Unlocked },
                                    Time = l.TimeLimit,
                                };

                                saveLevelDescription(gamedataFolder, l.Id, l.Description);
                                return level;
                            })
                            .ToList(),
                    };
                    return set;
                })
                .ToList(),
            };

            // Serialize
            var serialized = serializer.Serialize(levelData);

            // WriteAll
            File.WriteAllText(Path.Combine(gamedataFolder.FullName, "leveldata.xml"), serialized);
        }
        
        private void saveLevelDescription(DirectoryInfo gamedataFolder, string levelId, LevelDescription levelDescription)
        {
            var levelBriefing = converter.Convert<LevelDescription, XmlBriefingRoot>(levelDescription);
            var serialized = serializer.Serialize(levelBriefing);
            File.WriteAllText(Path.Combine(gamedataFolder.FullName, "dialogs", "briefing", $"{levelId}.xml"), serialized);
        }
    }
}
