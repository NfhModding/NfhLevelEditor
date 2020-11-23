using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.GfxData
{
    internal class XmlGfxObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlArray("gfxdata")]
        [XmlArrayItem("file")]
        public List<XmlGfxFile> Files { get; set; } = new();
    }
}
