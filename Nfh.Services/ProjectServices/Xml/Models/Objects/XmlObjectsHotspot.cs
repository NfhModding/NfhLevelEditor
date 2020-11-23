using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsHotspot
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("offset")]
        public XmlCoord Offset { get; set; } = XmlCoord.Zero;
    }
}