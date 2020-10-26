using System.Collections.Generic;

namespace Nfh.Domain.Models.Meta
{
    public class SeasonPack
    {
        public IDictionary<string, (Season Season, int Index)> Seasons { get; set; }
            = new Dictionary<string, (Season Season, int Index)>();
    }
}
