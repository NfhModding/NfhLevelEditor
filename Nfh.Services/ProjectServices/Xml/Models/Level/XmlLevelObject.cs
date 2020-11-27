﻿using Format.Xml.Attributes;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Level
{
    internal class XmlLevelObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("layer")]
        public int Layer { get; set; }

        [XmlAttribute("position")]
        public XmlCoord? Position { get; set; } = null;

        // Note: In some rare cases the visible="true" is written in the XML, after deserialization they are disappearing (most likely it's not an error
        [XmlHideDefault(true)]
        [XmlAttribute("visible")]
        public bool Visible { get; set; } = true;
    }
}
