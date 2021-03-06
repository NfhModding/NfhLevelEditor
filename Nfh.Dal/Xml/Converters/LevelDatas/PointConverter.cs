﻿using System.Drawing;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class PointConverter : TypeConverterBase<Point, XmlCoord>
    {
        public override Point ConvertToDomain(XmlCoord xmlModel)
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

        public override XmlCoord ConvertToXml(Point domain) => new()
        {
            X = domain.X,
            Y = domain.Y,
        };
    }
}
