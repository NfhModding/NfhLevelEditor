﻿using System.Drawing;
using Nfh.Dal.Xml.Models.Common;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class SizeConverter : TypeConverterBase<Size, XmlCoord>
    {
        public override Size ConvertToDomain(XmlCoord coord) => new()
        {
            Width = coord.X,
            Height = coord.Y,
        };

        public override XmlCoord ConvertToXml(Size size) => new()
        {
            X = size.Width,
            Y = size.Height,
        };
    }
}
