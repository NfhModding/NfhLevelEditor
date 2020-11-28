using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Objects
{
    internal class XmlObjectsContent
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("count")]
        public int Amount { get; set; }
    }
}