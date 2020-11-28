using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.GfxData
{
#pragma warning disable CS8625
    [XmlRoot(null)]
    internal class XmlGfxRoot
    {
        [XmlElement("object")]
        public List<XmlGfxObject> Objects { get; set; } = new();
    }
}
