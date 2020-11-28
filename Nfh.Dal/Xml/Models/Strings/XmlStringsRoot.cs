using System.Collections.Generic;
using System.Xml.Serialization;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Strings
{
    [XmlRoot("strings")]
    internal class XmlStringsRoot
    {
        [XmlElement("string")]
        public List<XmlString> Entries { get; set; } = new List<XmlString>();
    }
}
