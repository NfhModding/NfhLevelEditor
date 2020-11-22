using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlActorSpeed
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("speed")]
        public int Speed { get; set; }

        [XmlAttribute("start")]
        public int Start { get; set; }

        [XmlAttribute("noise")]
        public int Noise { get; set; }
    }
}