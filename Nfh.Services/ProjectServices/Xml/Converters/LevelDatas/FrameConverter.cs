using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
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
