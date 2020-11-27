using System.Collections.Generic;
using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    public class Room : IIdentifiable, ILocalizable
    {
        public string Id { get; }
        public Point Offset { get; set; }

        /// <summary>
        /// Probably used for pathfinding in the game.
        /// </summary>
        public Point[] Path { get; set; } = new Point[2];
        public Localization Localization { get; set; } = new();
        public IList<Floor> Floors { get; set; } = new List<Floor>();
        public IList<Wall> Walls { get; set; } = new List<Wall>();
        public IDictionary<string, LevelObject> Objects { get; set; } = new Dictionary<string, LevelObject>();

        public Room(string id)
        {
            Id = id;
        }
    }
}
