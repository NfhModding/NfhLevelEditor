﻿using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Level
{
    internal class XmlLevelNeighbor
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("costs")]
        public int Cost { get; set; }

        [XmlAttribute("doorin")]
        public string DoorIn { get; set; } = string.Empty;

        [XmlAttribute("doorout")]
        public string DoorOut { get; set; } = string.Empty;
    }
}
