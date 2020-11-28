using System.Xml.Serialization;
using Format.Xml.Attributes;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Level
{
    internal class XmlLevelDoor
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("layer")]
        public int Layer { get; set; }

        [XmlAttribute("position")]
        public XmlCoord Position { get; set; } = XmlCoord.Zero;

        [XmlHideDefault(true)]
        [XmlAttribute("visible")]
        public bool Visible { get; set; } = true;
    }
}
