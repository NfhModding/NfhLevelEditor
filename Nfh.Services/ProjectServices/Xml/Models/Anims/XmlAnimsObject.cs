using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    internal class XmlAnimsObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("region")]
        public List<XmlAnimsRegion> Regions { get; set; } = new();

        [XmlElement("animation")]
        public List<XmlAnimsAnimation> Animations { get; set; } = new();
    }
}
