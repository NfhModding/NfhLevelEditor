using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Meta
{
    internal class Set
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("state")]
        public StateAttribute State { get; set; }

        [XmlAttribute("nextset")]
        public string? NextSet { get; set; }

        [XmlElement("level")]
        public List<Level> Levels { get; set; } = new();
    }
}
