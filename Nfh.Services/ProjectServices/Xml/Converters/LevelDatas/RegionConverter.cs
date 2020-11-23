using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
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
