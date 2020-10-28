using System.Collections.Generic;

namespace Nfh.Domain.Models.InGame
{
    public class Visuals
    {
        public IList<VisualRegion> Regions { get; set; } = new List<VisualRegion>();
        public IDictionary<string, Animation> Animations { get; set; } = new Dictionary<string, Animation>();
    }
}
