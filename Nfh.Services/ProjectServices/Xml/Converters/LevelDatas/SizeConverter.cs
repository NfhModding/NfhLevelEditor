using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class SizeConverter : TypeConverterBase<Size, XmlCoord>
    {
        public override Size ConvertToDomain(XmlCoord xmlModel) => new()
        {
            Width = xmlModel.X,
            Height = xmlModel.Y,
        };

        public override XmlCoord ConvertToXml(Size domain) => new()
        {
            X = domain.Width,
            Y = domain.Height,
        };
    }
}
