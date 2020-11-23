﻿using Format.Xml.Attributes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Nfh.Services.ProjectServices.Xml.Models.Objects
{
    [XmlNoCompoundTag]
    internal class XmlObjectsObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("gfx")]
        public string Graphics { get; set; } = string.Empty;

        [XmlElement("hotspot")]
        public List<XmlObjectsHotspot> Hotspots { get; set; } = new();

        [XmlElement("flag")]
        public XmlObjectsFlag? Flag { get; set; } = null;

        [XmlElement("stdaction")]
        public XmlObjectsStdAction? StdAction { get; set; } = null;

        [XmlElement("content")]
        public List<XmlObjectsContent> Contents { get; set; } = new();

        [XmlElement("action")]
        public List<XmlObjectsAction> Actions { get; set; } = new();
    }
}
