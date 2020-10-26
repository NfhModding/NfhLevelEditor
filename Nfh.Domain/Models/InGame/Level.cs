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
        public string Id { get; }
        public LevelMeta Meta { get; set; }
        public Size Size { get; set; }
        public IDictionary<string, Room> Rooms { get; set; } = new Dictionary<string, Room>();
        public IDictionary<string, LevelObject> Object { get; set; } = new Dictionary<string, LevelObject>();
    }
}
