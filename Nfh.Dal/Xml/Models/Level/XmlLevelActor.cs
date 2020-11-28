using System.Xml.Serialization;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Level
{
    internal class XmlLevelActor
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("layer")]
        public int Layer { get; set; }

        [XmlAttribute("position")]
        public XmlCoord Position { get; set; } = XmlCoord.Zero;

        [XmlAttribute("animation")]
        public string? Animation { get; set; } = null;
    }
}
