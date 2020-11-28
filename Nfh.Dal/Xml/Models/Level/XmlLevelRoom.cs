using System.Collections.Generic;
using System.Xml.Serialization;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Level
{
    internal class XmlLevelRoom
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("offset")]
        public XmlCoord Offset { get; set; } = XmlCoord.Zero;

        [XmlAttribute("path1")]
        public XmlCoord Path1 { get; set; } = XmlCoord.Zero;

        [XmlAttribute("path2")]
        public XmlCoord Path2 { get; set; } = XmlCoord.Zero;

        [XmlElement("floor")]
        public List<XmlLevelFloor> Floors { get; set; } = new();

        [XmlElement("object")]
        public List<XmlLevelObject> Objects { get; set; } = new();

        [XmlElement("door")]
        public List<XmlLevelDoor> Doors { get; set; } = new();

        [XmlElement("neighbor")]
        public List<XmlLevelNeighbor> Neighbors { get; set; } = new();

        [XmlElement("actor")]
        public List<XmlLevelActor> Actors { get; set; } = new();
    }
}
