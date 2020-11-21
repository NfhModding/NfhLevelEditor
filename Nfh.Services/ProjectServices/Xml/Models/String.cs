using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models
{
    internal class String
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("category")]
        public string Category { get; set; } = string.Empty;

        [XmlAttribute("text")]
        public string Text { get; set; } = string.Empty;
    }
}
