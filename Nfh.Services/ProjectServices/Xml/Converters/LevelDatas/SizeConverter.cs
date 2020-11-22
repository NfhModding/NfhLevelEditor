using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    // ToDo check
    internal class SizeConverter : TypeConverterBase<Size, Coord>
    {
        public override Size convertToDomain(Coord xmlModel) => new()
        {
            Width = xmlModel.X,
            Height = xmlModel.Y,
        };

        public override Coord convertToXml(Size domain) => new()
        {
            X = domain.Width,
            Y = domain.Height,
        };
    }
}
