using System.Xml.Serialization;
using Format.Xml.Attributes;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Anims
{
    internal class XmlAnimsRegion
    {
        public enum RegionType
        {
            [XmlEnum(Name = "simple")] Simple,
            [XmlEnum(Name = "text")] Text,
        }

        [XmlAttribute("position")]
        public XmlCoord Position { get; set; } = XmlCoord.Zero;

        [XmlAttribute("size")]
        public XmlCoord Size { get; set; } = XmlCoord.Zero;

        [XmlAttribute("type")]
        [XmlHideDefault]
        public RegionType Type { get; set; } = RegionType.Simple;
    }
}
