using Format.Xml.Attributes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    [XmlNoCompoundTag]
    internal class XmlAnimsAnimation
    {
        public enum Kind
        {
            [XmlEnum(Name = "oneshot")] OneShot,
            [XmlEnum(Name = "loop")] Loop,
        }

        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("type")]
        public Kind Type { get; set; } = Kind.OneShot;

        [XmlElement("frame")]
        public List<XmlAnimsFrame> Frames { get; set; } = new();
    }
}
