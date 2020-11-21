using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Meta
{
    [XmlRoot("leveldata")]
    internal class LevelDataRoot
    {
        [XmlElement("set")]
        public List<Set> Sets { get; set; } = new();
    }
}
