using System.Collections.Generic;
using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    public class Room : IIdentifiable
    {
        public string Id { get; }
        public Point Position { get; set; }
        /// <summary>
        /// Probably used for pathfinding in the game.
        /// </summary>
        public Point[] Path { get; } = new Point[2];
        public Localization Localization { get; set; }
        public IList<Floor> Floors { get; set; } = new List<Floor>();
        public IList<Wall> Walls { get; set; } = new List<Wall>();
        public IDictionary<string, LevelObject> Objects { get; set; } = new Dictionary<string, LevelObject>();
    }
}
