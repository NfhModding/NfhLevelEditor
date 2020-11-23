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
        public XmlCoord? Hotspot { get; set; } = null;

        [XmlElement("hotspot")]
        public List<XmlObjectsHotspot> Hotspots { get; set; } = new();

        [XmlElement("speed")]
        public List<XmlObjectsActorSpeed> Speeds { get; set; } = new();

        [XmlElement("stdaction")]
        public XmlObjectsStdAction? StdAction { get; set; } = null;

        [XmlElement("action")]
        public List<XmlObjectsAction> Actions { get; set; } = new();
    }
}