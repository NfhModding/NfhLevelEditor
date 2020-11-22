using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.GfxData
{
    internal class GfxObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlArray("gfxdata")]
        [XmlArrayItem("file")]
        public List<GfxFile> Files { get; set; } = new();
    }
}
