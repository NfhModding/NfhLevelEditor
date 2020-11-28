using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Common
{
    internal class XmlString
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("category")]
        public string Category { get; set; } = string.Empty;

        [XmlAttribute("text")]
        public string Text { get; set; } = string.Empty;
    }
}
