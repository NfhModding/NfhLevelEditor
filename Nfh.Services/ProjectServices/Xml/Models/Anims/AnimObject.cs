using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    internal class AnimObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("region")]
        public List<XmlRegion> Regions { get; set; } = new();

        [XmlElement("animation")]
        public List<XmlAnimation> Animations { get; set; } = new();
    }
}
