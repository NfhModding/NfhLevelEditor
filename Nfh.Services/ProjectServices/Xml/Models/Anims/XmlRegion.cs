using Format.Xml.Attributes;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    internal class XmlRegion
    {
        public enum RegionType
        {
            [XmlEnum(Name = "simple")] Simple,
            [XmlEnum(Name = "text")] Text,
        }

        [XmlAttribute("position")]
        public Coord Position { get; set; } = Coord.Zero;

        [XmlAttribute("size")]
        public Coord Size { get; set; } = Coord.Zero;

        [XmlAttribute("type")]
        [XmlHideDefault]
        public RegionType Type { get; set; } = RegionType.Simple;
    }
}
