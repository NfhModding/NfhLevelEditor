using Nfh.Domain.Models.InGame;
using System;
using Nfh.Dal.Xml.Models.Anims;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class AnimationKindConverter : TypeConverterBase<Animation.Kind, XmlAnimsAnimation.Kind>
    {
        public override Animation.Kind ConvertToDomain(XmlAnimsAnimation.Kind animType) => animType switch
        {
            XmlAnimsAnimation.Kind.OneShot => Animation.Kind.SingleShot,
            XmlAnimsAnimation.Kind.Loop => Animation.Kind.Loop,
            _ => throw new NotImplementedException(),
        };

        public override XmlAnimsAnimation.Kind ConvertToXml(Animation.Kind domain)
        {
            throw new NotImplementedException();
        }
    }
}
