using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    [XmlRoot("all_objects")]
    internal class AnimsRoot
    {
        [XmlElement("object")]
        public List<AnimObject> Objects { get; set; } = new();
    }
}
