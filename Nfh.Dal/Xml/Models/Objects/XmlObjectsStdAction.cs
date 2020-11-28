using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Objects
{
    internal class XmlObjectsStdAction
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}