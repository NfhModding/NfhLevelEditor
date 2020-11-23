using System.Collections.Generic;

namespace Nfh.Domain.Models.InGame
{
    public class Visuals : IIdentifiable
    {
        public string Id { get; }

        public IList<VisualRegion> Regions { get; set; } 
            = new List<VisualRegion>();

        public IDictionary<string, Animation> Animations { get; set; }
            = new Dictionary<string, Animation>();

        public Visuals(string id)
        {
            Id = id;
        }
    }
}
