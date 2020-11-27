using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class FloorsConverter : TypeConverterBase<List<Floor>, List<XmlLevelFloor>>
    {
        private readonly IConverter converter;

        public FloorsConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override List<Floor> ConvertToDomain(List<XmlLevelFloor> floors) => floors
               .Where(f => !f.Wall)
               .Select(f => new Floor
               {
                   Bounds = new Rectangle
                   {
                       Size = converter.Convert<XmlCoord, Size>(f.Size),
                       Location = converter.Convert<XmlCoord, Point>(f.Offset),
                   },
                   Hotspot = f.Hotspot is null ? Point.Empty : converter.Convert<XmlCoord, Point>(f.Hotspot),
               })
               .ToList();

        public override List<XmlLevelFloor> ConvertToXml(List<Floor> floors) => floors
            .Select(f => new XmlLevelFloor()
            {
                Wall = false,
                Size = converter.Convert<Size, XmlCoord>(f.Bounds.Size),
                Offset = converter.Convert<Point, XmlCoord>(f.Bounds.Location),
                Hotspot = f.Hotspot == Point.Empty ? null : converter.Convert<Point, XmlCoord >(f.Hotspot),
            })
            .ToList();
    }
}
