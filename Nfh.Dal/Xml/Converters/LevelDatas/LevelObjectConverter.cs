using Nfh.Domain.Models.InGame;
using System.Drawing;
using Nfh.Dal.Xml.Models.Common;
using Nfh.Dal.Xml.Models.Level;

namespace Nfh.Dal.Xml.Converters.LevelDatas
{
    internal class LevelObjectConverter : TypeConverterBase<LevelObject, XmlLevelObject>
    {
        private readonly IConverter converter;

        public LevelObjectConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override LevelObject ConvertToDomain(XmlLevelObject obj) => new(obj.Name)
        {
            Layer = obj.Layer,
            Position = converter.Convert<XmlCoord, Point>(obj.Position), // ToDo nullable things in converters
            // The rest is connected later
        };

        public override XmlLevelObject ConvertToXml(LevelObject levelObject) => new()
        {
            Name = levelObject.Id,
            Layer = levelObject.Layer,
            Position = converter.Convert<Point, XmlCoord>(levelObject.Position),
            Visible = true, // default
        };
    }
}
