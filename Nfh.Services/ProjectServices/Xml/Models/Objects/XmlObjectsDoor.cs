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
        public List<XmlHotspot> Hotspots { get; set; } = new();

        [XmlElement("stdaction")]
        public XmlStdAction StdAction { get; set; }

        [XmlElement("action")]
        public List<XmlAction> Actions { get; set; } = new();

        [XmlElement("flag")]
        public XmlFlag Flag { get; set; }
    }
}