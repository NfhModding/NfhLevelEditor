using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class VisualsConverter : TypeConverterBase<Visuals, XmlAnimsObject>
    {
        private readonly IConverter converter;

        public VisualsConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Visuals convertToDomain(XmlAnimsObject animObject) => new(animObject.Name)
        {
            Regions = animObject.Regions.Select(converter.Convert<XmlAnimsRegion, VisualRegion>).ToList(),
            Animations = animObject.Animations.Select(converter.Convert<XmlAnimsAnimation, Animation>).ToDictionary(v => v.Id, v => v),
        };

        public override XmlAnimsObject convertToXml(Visuals domain)
        {
            throw new NotImplementedException();
        }
    }
}
