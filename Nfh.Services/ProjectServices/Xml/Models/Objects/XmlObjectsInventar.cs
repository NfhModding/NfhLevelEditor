using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsInventar
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("flag")]
        public XmlObjectsFlag? Flag { get; set; } = null;

        [XmlElement("image")]
        public List<XmlObjectsInventarImage> Images { get; set; } = new();
    }
}