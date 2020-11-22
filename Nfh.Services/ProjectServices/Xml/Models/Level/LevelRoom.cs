using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Level
{
    internal class LevelRoom
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("offset")]
        public Coord Offset { get; set; } = Coord.Zero;

        [XmlAttribute("path1")]
        public Coord Path1 { get; set; } = Coord.Zero;

        [XmlAttribute("path2")]
        public Coord Path2 { get; set; } = Coord.Zero;

        [XmlElement("floor")]
        public List<LevelFloor> Floors { get; set; } = new();

        [XmlElement("object")]
        public List<XmlLevelObject> Objects { get; set; } = new();

        [XmlElement("door")]
        public List<LevelDoor> Doors { get; set; } = new();

        [XmlElement("neighbor")]
        public List<LevelNeighbor> Neighbors { get; set; } = new();

        [XmlElement("actor")]
        public List<LevelActor> Actors { get; set; } = new();
    }
}
