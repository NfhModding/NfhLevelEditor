using System;

namespace Nfh.Domain.Models.Meta
{
    /// <summary>
    /// The metadata about a single level.
    /// </summary>
    public class LevelMeta
    {
        public LevelDescription Description { get; set; }
        public bool Unlocked { get; set; }
        public int TrickCount { get; set; }
        /// <summary>
        /// The time given to the player to finish the level.
        /// Can be null, if there's no time limit.
        /// </summary>
        public TimeSpan? TimeLimit { get; set; }
    }
}
