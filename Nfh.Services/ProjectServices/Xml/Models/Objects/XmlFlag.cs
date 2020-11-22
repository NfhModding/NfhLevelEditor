using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlFlag
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}