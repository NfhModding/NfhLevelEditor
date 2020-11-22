using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    internal class XmlAction
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("actor")]
        public string Actor { get; set; } = string.Empty;

        [XmlAttribute("time")]
        public XmlTime Time { get; set; } = XmlTime.Auto;

        [XmlAttribute("objanim")]
        public string? ObjectAnimation { get; set; } = null;

        [XmlAttribute("objnextanim")]
        public string? ObjectNextAnimation { get; set; } = null;

        [XmlAttribute("actoranim")]
        public string? ActorAnimation { get; set; } = null;

        [XmlAttribute("actornextanim")]
        public string? ActorNextAnimation { get; set; } = null;

        [XmlAttribute("noise")]
        public int? Noise { get; set; } = null;

        [XmlAttribute("behavior")]
        public string? Behavior { get; set; } = null;

        [XmlAttribute("behavioractor")]
        public string? BehaviorActor { get; set; } = null;

        [XmlAttribute("always")]
        public bool? Always { get; set; } = null;
    }
}