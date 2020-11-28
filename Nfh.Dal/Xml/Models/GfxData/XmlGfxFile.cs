using System.Xml.Serialization;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.GfxData
{
    internal class XmlGfxFile
    {
        [XmlAttribute("image")]
        public string Image { get; set; } = string.Empty;

        [XmlAttribute("offset")]
        public XmlCoord Offset { get; set; } = XmlCoord.Zero;
    }
}
