using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Models.Briefing
{
    [XmlRoot("strings")]
    internal class XmlBriefingRoot
    {
        [XmlElement("string")]
        public List<XmlString> raw = new();

        [XmlIgnore]
        public string Title {
            get => get("title").Text;
            set => get("title").Text = value;
        }

        [XmlIgnore]
        public string Hint {
            get => get("hinttext").Text;
            set => get("hinttext").Text = value;
        }

        [XmlIgnore]
        public string ThumbnailDescription {
            get => get("pictext").Text;
            set => get("pictext").Text = value;
        }

        [XmlIgnore]
        public string LevelDescription {
            get => get("briefing").Text;
            set => get("briefing").Text = value;
        }

        private XmlString get(string name)
        {
            var result = raw.FirstOrDefault(x => x.Name == name);
            if (result == null)
            {
                result = new XmlString { Name = name, Category = "briefing", Text = string.Empty };
                raw.Add(result);
            }
            return result;
        }
    }
}
