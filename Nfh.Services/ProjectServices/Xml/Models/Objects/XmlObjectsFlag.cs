using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsFlag
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}