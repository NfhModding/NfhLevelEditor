using Nfh.Domain.Models.InGame;
using System;
using Nfh.Dal.Xml.Models.Anims;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class FrameConverter : TypeConverterBase<Animation.Frame, XmlAnimsFrame>
    {
        public override Animation.Frame ConvertToDomain(XmlAnimsFrame frame) => new()
        {
            ImagePath = frame.Graphics,
            SoundPath = frame.Sound,
            // ImageOffset, later (need gfxdata)
        };

        public override XmlAnimsFrame ConvertToXml(Animation.Frame domain)
        {
            throw new NotImplementedException();
        }
    }
}
