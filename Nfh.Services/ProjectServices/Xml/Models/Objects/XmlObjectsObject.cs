using Format.Xml.Attributes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    [XmlNoCompoundTag]
    internal class XmlObjectsObject : XmlObjectsBase
    {
        [XmlElement("flag")]
        public XmlObjectsFlag? Flag { get; set; } = null;

        [XmlElement("stdaction")]
        public XmlObjectsStdAction? StdAction { get; set; } = null;

        [XmlElement("content")]
        public List<XmlObjectsContent> Contents { get; set; } = new();

        [XmlElement("action")]
        public List<XmlObjectsAction> Actions { get; set; } = new();
    }
}
