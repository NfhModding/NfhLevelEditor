using Nfh.Domain.Models.InGame;
using System;
using System.Drawing;
using Nfh.Dal.Xml.Models.Anims;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class RegionConverter : TypeConverterBase<VisualRegion, XmlAnimsRegion>
    {
        private readonly IConverter converter;

        public RegionConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override VisualRegion ConvertToDomain(XmlAnimsRegion region) => new()
        {
            Bounds = new()
            {
                Size = converter.Convert<XmlCoord, Size>(region.Size),
                Location = converter.Convert<XmlCoord, Point>(region.Position),
            },
            Text = region.Type == XmlAnimsRegion.RegionType.Text,
        };

        public override XmlAnimsRegion ConvertToXml(VisualRegion domain)
        {
            throw new NotImplementedException();
        }
    }
}
