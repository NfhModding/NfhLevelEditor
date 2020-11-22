using Format.Xml.Attributes;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    [XmlNoCompoundTag]
    internal class XmlObjectsActor
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("gfx")]
        public string? Graphics { get; set; } = null;

        [XmlAttribute("hotspot")]
        public Coord? Hotspot { get; set; } = null;

        [XmlElement("hotspot")]
        public List<XmlHotspot> Hotspots { get; set; } = new();

        [XmlElement("speed")]
        public List<XmlActorSpeed> Speeds { get; set; } = new();

        [XmlElement("stdaction")]
        public XmlStdAction? StdAction { get; set; } = null;

        [XmlElement("action")]
        public List<XmlAction> Actions { get; set; } = new();
    }
}