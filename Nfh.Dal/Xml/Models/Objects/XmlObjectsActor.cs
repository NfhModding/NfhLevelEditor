using System.Collections.Generic;
using System.Xml.Serialization;
using Format.Xml.Attributes;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Objects
{
    [XmlNoCompoundTag]
    internal class XmlObjectsActor : XmlObjectsBase
    {
        [XmlAttribute("hotspot")]
        public XmlCoord? Hotspot { get; set; } = null;

        [XmlElement("speed")]
        public List<XmlObjectsActorSpeed> Speeds { get; set; } = new();

        [XmlElement("stdaction")]
        public XmlObjectsStdAction? StdAction { get; set; } = null;

        [XmlElement("action")]
        public List<XmlObjectsAction> Actions { get; set; } = new();
    }
}