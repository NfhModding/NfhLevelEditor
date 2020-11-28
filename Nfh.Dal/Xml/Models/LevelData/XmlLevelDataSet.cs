using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.LevelData
{
    internal class XmlLevelDataSet
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("state")]
        public XmlLevelDataStateAttribute State { get; set; } = new();

        [XmlAttribute("nextset")]
        public string? NextSet { get; set; }

        [XmlElement("level")]
        public List<XmlLevelDataLevel> Levels { get; set; } = new();
    }
}
