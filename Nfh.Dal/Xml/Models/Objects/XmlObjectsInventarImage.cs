﻿using System.Xml.Serialization;

namespace Nfh.Dal.Xml.Models.Objects
{
    internal class XmlObjectsInventarImage
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("gfx")]
        public string Graphics { get; set; } = string.Empty;
    }
}