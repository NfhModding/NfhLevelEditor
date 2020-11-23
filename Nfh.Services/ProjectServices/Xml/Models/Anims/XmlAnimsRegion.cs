using Format.Xml.Attributes;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
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
