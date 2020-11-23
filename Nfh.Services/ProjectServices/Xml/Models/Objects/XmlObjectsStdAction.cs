using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsStdAction
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}