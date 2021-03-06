﻿using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Objects
{
    internal class XmlObjectsActorSpeed
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