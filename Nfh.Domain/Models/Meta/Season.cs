using System.Collections.Generic;

namespace Nfh.Domain.Models.Meta
{
    public class Season : IIdentifiable
    {
        public string Id { get; }
        public bool Unlocked { get; set; }
        public Season? Unlocks { get; set; }
        public IDictionary<string, (LevelMeta Level, int Index)> Levels { get; set; } 
            = new Dictionary<string, (LevelMeta Level, int Index)>();

        public Season(string id)
        {
            Id = id;
        }
    }
}
