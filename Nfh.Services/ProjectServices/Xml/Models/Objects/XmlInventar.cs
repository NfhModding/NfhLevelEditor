using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlInventar
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("flag")]
        public XmlFlag? Flag { get; set; } = null;

        [XmlElement("image")]
        public List<XmlInventarImage> Images { get; set; } = new();
    }
}