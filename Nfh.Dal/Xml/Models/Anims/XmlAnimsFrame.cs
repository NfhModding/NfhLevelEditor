using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Anims
{
    internal class XmlAnimsFrame
    {
        [XmlAttribute("gfx")]
        public string Graphics { get; set; } = string.Empty;

        [XmlAttribute("sfx")]
        public string? Sound { get; set; } = null;
    }
}
