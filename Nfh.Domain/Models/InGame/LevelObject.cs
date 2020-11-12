using System.Collections.Generic;
using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    public class LevelObject : IIdentifiable
    {
        public string Id { get; }
        public int Layer { get; set; }
        public Point Position { get; set; }
        public Localization Localization { get; set; }
        public IDictionary<string, Point> InteractionSpots { get; set; } = new Dictionary<string, Point>();
        public Visuals? Visuals { get; set; }

        public LevelObject(string id)
        {
            Id = id;
        }
    }
}
