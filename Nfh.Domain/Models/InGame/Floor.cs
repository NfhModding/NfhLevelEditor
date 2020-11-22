using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    /// <summary>
    /// A walkable place inside a room.
    /// </summary>
    public class Floor
    {
        /// <summary>
        /// Relative to the <see cref="Room"/> position.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Undetermined, probably the place to put items on the floor?
        /// </summary>
        public Point Hotspot { get; set; }
    }
}
