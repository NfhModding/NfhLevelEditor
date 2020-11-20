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
        public LevelMeta Meta { get; set; }
        public Size Size { get; set; }
        public IDictionary<string, Room> Rooms { get; set; } = new Dictionary<string, Room>();
        public IDictionary<string, LevelObject> Objects { get; set; } = new Dictionary<string, LevelObject>();
    }
}
