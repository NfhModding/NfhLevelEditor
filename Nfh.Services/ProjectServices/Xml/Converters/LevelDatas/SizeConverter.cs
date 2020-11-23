using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    // ToDo check
    internal class SizeConverter : TypeConverterBase<Size, XmlCoord>
    {
        public override Size convertToDomain(XmlCoord xmlModel) => new()
        {
            Width = xmlModel.X,
            Height = xmlModel.Y,
        };

        public override XmlCoord convertToXml(Size domain) => new()
        {
            X = domain.Width,
            Y = domain.Height,
        };
    }
}
