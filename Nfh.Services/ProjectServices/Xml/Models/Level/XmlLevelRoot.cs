using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Level
{
    [XmlRoot("level")]
    internal class XmlLevelRoot
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("size")]
        public XmlCoord Size { get; set; } = XmlCoord.Zero;

        [XmlAttribute("angrytime")]
        public int AngryTime { get; set; }

        [XmlElement("object")]
        public List<XmlLevelObject> Objects { get; set; } = new();

        [XmlElement("room")]
        public List<XmlLevelRoom> Rooms { get; set; } = new();
    }
}
