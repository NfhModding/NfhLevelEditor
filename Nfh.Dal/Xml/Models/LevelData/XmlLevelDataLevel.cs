using System;
using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.LevelData
{
    internal class XmlLevelDataLevel
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("state")]
        public XmlLevelDataStateAttribute State { get; set; } = new();

        [XmlAttribute("reachable")]
        public int Reachable { get; set; }

        [XmlAttribute("minquota")]
        public int MinQuota { get; set; }

        [XmlAttribute("time")]
        public TimeSpan? Time { get; set; }
    }
}
