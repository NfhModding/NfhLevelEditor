using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Level
{
    [XmlRoot("level")]
    internal class LevelRoot
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("size")]
        public Coord Size { get; set; } = Coord.Zero;

        [XmlAttribute("angrytime")]
        public int AngryTime { get; set; }

        [XmlElement("object")]
        public List<XmlLevelObject> Objects { get; set; } = new();

        [XmlElement("room")]
        public List<LevelRoom> Rooms { get; set; } = new();
    }
}
