using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsDoor
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("gfx")]
        public string Graphics { get; set; } = string.Empty;

        [XmlElement("hotspot")]
        public List<XmlObjectsHotspot> Hotspots { get; set; } = new();

        [XmlElement("stdaction")]
        public XmlObjectsStdAction StdAction { get; set; } = new();

        [XmlElement("action")]
        public List<XmlObjectsAction> Actions { get; set; } = new();

        [XmlElement("flag")]
        public XmlObjectsFlag Flag { get; set; } = new();
    }
}