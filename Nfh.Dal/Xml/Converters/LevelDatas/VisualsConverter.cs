﻿using Nfh.Domain.Models.InGame;
using System;
using System.Linq;
using Nfh.Dal.Xml.Models.Anims;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class VisualsConverter : TypeConverterBase<Visuals, XmlAnimsObject>
    {
        private readonly IConverter converter;

        public VisualsConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Visuals ConvertToDomain(XmlAnimsObject animObject) => new(animObject.Name)
        {
            Regions = animObject.Regions.Select(converter.Convert<XmlAnimsRegion, VisualRegion>).ToList(),
            Animations = animObject.Animations.Select(converter.Convert<XmlAnimsAnimation, Animation>).ToDictionary(v => v.Id, v => v),
        };

        public override XmlAnimsObject ConvertToXml(Visuals domain)
        {
            throw new NotImplementedException();
        }
    }
}
