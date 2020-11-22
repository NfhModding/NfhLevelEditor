using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class PointConverter : TypeConverterBase<Point, Coord>
    {
        public override Point convertToDomain(Coord xmlModel) => new()
        {
            X = xmlModel.X,
            Y = xmlModel.Y,
        };

        public override Coord convertToXml(Point domain) => new()
        {
            X = domain.X,
            Y = domain.Y,
        };
    }
}
