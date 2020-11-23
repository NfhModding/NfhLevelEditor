using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Meta
{
    [XmlRoot("leveldata")]
    internal class XmlLevelDataRoot
    {
        [XmlElement("set")]
        public List<XmlLevelDataSet> Sets { get; set; } = new();
    }
}
