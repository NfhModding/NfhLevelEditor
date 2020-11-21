using System;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Meta
{
    internal class Level
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("state")]
        public StateAttribute State { get; set; }

        [XmlAttribute("reachable")]
        public int Reachable { get; set; }

        [XmlAttribute("minquota")]
        public int MinQuota { get; set; }

        [XmlAttribute("time")]
        public TimeSpan? Time { get; set; }
    }
}
