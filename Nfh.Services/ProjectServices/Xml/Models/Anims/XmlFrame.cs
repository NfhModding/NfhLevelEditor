using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Anims
{
    internal class XmlFrame
    {
        [XmlAttribute("gfx")]
        public string Graphics { get; set; } = string.Empty;

        [XmlAttribute("sfx")]
        public string? Sound { get; set; } = null;
    }
}
