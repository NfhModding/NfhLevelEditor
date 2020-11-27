using Nfh.Domain.Models.Meta;
using System.Collections.Generic;
using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    /// <summary>
    /// In-game representation of a level.
    /// </summary>
    public class Level : IIdentifiable
    {
        public string Id => Meta.Id;
        public string Name { get; set; } = string.Empty;
        public int AngryTime { get; set; }
        public LevelMeta Meta { get; set; }
        public Size Size { get; set; }
        public IDictionary<string, Room> Rooms { get; set; } = new Dictionary<string, Room>();
        public IDictionary<string, LevelObject> Objects { get; set; } = new Dictionary<string, LevelObject>();
        public IDictionary<string, Localization> ObjectDependentLocalizations { get; set; } = new Dictionary<string, Localization>();
    }
}
