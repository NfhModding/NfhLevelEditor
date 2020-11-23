using Format.Xml.Attributes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    [XmlOptionalRoot("objects")]
    internal class XmlObjectsRoot
    {
        [XmlElement("icon")]
        public List<XmlObjectsIcon> Icons { get; set; } = new();

        [XmlElement("actor")]
        public List<XmlObjectsActor> Actors { get; set; } = new();

        [XmlElement("object")]
        public List<XmlObjectsObject> Objects { get; set; } = new();

        [XmlElement("inventar")]
        public List<XmlObjectsInventar> Inventars { get; set; } = new();

        [XmlElement("door")]
        public List<XmlObjectsDoor> Doors { get; set; } = new();
    }
}
