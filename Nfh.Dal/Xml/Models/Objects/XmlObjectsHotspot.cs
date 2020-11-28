using System.Xml.Serialization;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Objects
{
    internal class XmlObjectsHotspot
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("offset")]
        public XmlCoord Offset { get; set; } = XmlCoord.Zero;
    }
}