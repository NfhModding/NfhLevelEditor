using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.GfxData
{
    [XmlRoot(null)]
    internal class GfxDataRoot
    {
        [XmlElement("object")]
        public List<GfxObject> Objects { get; set; } = new();
    }
}
