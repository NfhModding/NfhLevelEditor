using Format.Xml.Attributes;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Level
{
    internal class XmlLevelObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("layer")]
        public int Layer { get; set; }

        [XmlAttribute("position")]
        public Coord? Position { get; set; } = null;

        [XmlHideDefault(true)]
        [XmlAttribute("visible")]
        public bool Visible { get; set; } = true;
    }
}
