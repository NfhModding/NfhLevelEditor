using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Objects
{
    internal abstract class XmlObjectsBase
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("gfx")]
        public string? Graphics { get; set; } = null;

        [XmlElement("hotspot")]
        public List<XmlObjectsHotspot> Hotspots { get; set; } = new();
    }
}
