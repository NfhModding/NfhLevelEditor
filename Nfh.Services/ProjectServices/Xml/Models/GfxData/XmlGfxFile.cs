using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.GfxData
{
    internal class XmlGfxFile
    {
        [XmlAttribute("image")]
        public string Image { get; set; } = string.Empty;

        [XmlAttribute("offset")]
        public XmlCoord Offset { get; set; } = XmlCoord.Zero;
    }
}
