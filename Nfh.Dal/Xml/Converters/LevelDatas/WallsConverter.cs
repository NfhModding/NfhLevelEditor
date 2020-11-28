using Nfh.Domain.Models.InGame;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Nfh.Dal.Xml.Models.Common;
using Nfh.Dal.Xml.Models.Level;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class WallsConverter : TypeConverterBase<IList<Wall>, IList<XmlLevelFloor>>
    {
        private readonly IConverter converter;

        public WallsConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override IList<Wall> ConvertToDomain(IList<XmlLevelFloor> floors) => floors
                 .Where(f => f.Wall)
                 .Select(f => new Wall
                 {
                     Bounds = new Rectangle
                     {
                         Size = converter.Convert<XmlCoord, Size>(f.Size),
                         Location = converter.Convert<XmlCoord, Point>(f.Offset),
                     },
                 })
                 .ToList();

        public override IList<XmlLevelFloor> ConvertToXml(IList<Wall> walls) => walls
            .Select(w => new XmlLevelFloor
            {
                Wall = true,
                Size = converter.Convert<Size, XmlCoord>(w.Bounds.Size),
                Offset = converter.Convert<Point, XmlCoord>(w.Bounds.Location),
                // Walls do not have Hotspots, only floors do
            })
            .ToList();
    }
}
