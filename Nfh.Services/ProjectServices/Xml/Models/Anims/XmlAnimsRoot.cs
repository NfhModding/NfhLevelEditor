using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    [XmlRoot("all_objects")]
    internal class XmlAnimsRoot
    {
        [XmlElement("object")]
        public List<XmlAnimsObject> Objects { get; set; } = new();
    }
}
