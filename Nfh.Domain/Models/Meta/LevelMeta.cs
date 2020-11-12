using System;

namespace Nfh.Domain.Models.Meta
{
    /// <summary>
    /// The metadata about a single level.
    /// </summary>
    public class LevelMeta : IIdentifiable
    {
        public string Id { get; }
        public LevelDescription Description { get; set; }
        public bool Unlocked { get; set; }
        public int TrickCount { get; set; }
        /// <summary>
        /// Minimal view percentage to mark the level as complete.
        /// </summary>
        public int MinPercent { get; set; }
        /// <summary>
        /// The time given to the player to finish the level.
        /// Can be null, if there's no time limit.
        /// </summary>
        public TimeSpan? TimeLimit { get; set; }

        public LevelMeta(string id)
        {
            Id = id;
        }
    }
}
