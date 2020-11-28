using Nfh.Domain.Models.InGame;
using System;
using System.Linq;
using Nfh.Dal.Xml.Models.Anims;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class AnimationConverter : TypeConverterBase<Animation, XmlAnimsAnimation>
    {
        private readonly IConverter converter;

        public AnimationConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Animation ConvertToDomain(XmlAnimsAnimation animation) => new(animation.Name)
        {
            Frames = animation.Frames.Select(converter.Convert<XmlAnimsFrame, Animation.Frame>).ToList(),
            Kind_ = converter.Convert<XmlAnimsAnimation.Kind, Animation.Kind>(animation.Type),
        };

        public override XmlAnimsAnimation ConvertToXml(Animation domain)
        {
            throw new NotImplementedException();
        }
    }
}
