using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Strings
{
    [XmlRoot("strings")]
    internal class StringsRoot
    {
        [XmlElement("string")]
        public List<XmlString> Entries { get; set; } = new List<XmlString>();
    }
}
