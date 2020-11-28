using System.Xml.Serialization;
using Format.Xml.Attributes;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Level
{
    [XmlAllowDoubleCompoundClose]
    internal class XmlLevelFloor
    {
        [XmlAttribute("offset")]
        public XmlCoord Offset { get; set; } = XmlCoord.Zero;

        [XmlAttribute("size")]
        public XmlCoord Size { get; set; } = XmlCoord.Zero;

        [XmlAttribute("wall")]
        public bool Wall { get; set; }

        [XmlAttribute("hotspot")]
        public XmlCoord? Hotspot { get; set; } = null;
    }
}
