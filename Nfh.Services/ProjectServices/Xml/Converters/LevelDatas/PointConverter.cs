using Nfh.Services.ProjectServices.Xml.Models.Common;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class PointConverter : TypeConverterBase<Point, XmlCoord>
    {
        public override Point convertToDomain(XmlCoord xmlModel)
        {
            // ToDo convert back -> null iff object.name = house
            if (xmlModel is null)
                return new(0, 0);

            return new()
            {
                X = xmlModel.X,
                Y = xmlModel.Y,
            };
        }

        public override XmlCoord convertToXml(Point domain) => new()
        {
            X = domain.X,
            Y = domain.Y,
        };
    }
}
