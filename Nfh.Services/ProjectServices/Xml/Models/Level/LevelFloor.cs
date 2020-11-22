using Format.Xml.Attributes;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Level
{
    [XmlAllowDoubleCompoundClose]
    internal class LevelFloor
    {
        [XmlAttribute("offset")]
        public Coord Offset { get; set; } = Coord.Zero;

        [XmlAttribute("size")]
        public Coord Size { get; set; } = Coord.Zero;

        [XmlAttribute("wall")]
        public bool Wall { get; set; }

        [XmlAttribute("hotspot")]
        public Coord? Hotspot { get; set; } = null;
    }
}
