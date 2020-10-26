using System.Collections.Generic;

namespace Nfh.Domain.Models.InGame
{
    public class Visuals
    {
        public IList<VisualRegion> Regions { get; set; }
        public IList<Animation> Animations { get; set; }
    }
}
