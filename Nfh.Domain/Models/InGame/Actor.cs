using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    public class Actor : LevelObject
    {
        public Point Hotspot { get; set; }

        public Actor(string id) 
            : base(id)
        {
        }
    }
}
