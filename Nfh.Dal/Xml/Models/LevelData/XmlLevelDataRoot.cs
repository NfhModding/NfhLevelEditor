using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.LevelData
{
    [XmlRoot("leveldata")]
    internal class XmlLevelDataRoot
    {
        [XmlElement("set")]
        public List<XmlLevelDataSet> Sets { get; set; } = new();
    }
}
