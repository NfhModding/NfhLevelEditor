using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlObjectsDoor : XmlObjectsBase
    {
        [XmlElement("stdaction")]
        public XmlObjectsStdAction StdAction { get; set; } = new();

        [XmlElement("action")]
        public List<XmlObjectsAction> Actions { get; set; } = new();

        [XmlElement("flag")]
        public XmlObjectsFlag Flag { get; set; } = new();
    }
}