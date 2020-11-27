using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System.Drawing;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class DoorConverter : TypeConverterBase<Door, XmlLevelDoor>
    {
        private readonly IConverter converter;

        public DoorConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Door ConvertToDomain(XmlLevelDoor door) => new(door.Name)
        {
            Layer = door.Layer,
            Position = converter.Convert<XmlCoord, Point>(door.Position),
            // The rest is connected later, here, there are not enough information
        };

        public override XmlLevelDoor ConvertToXml(Door door) => new()
        {
            Name = door.Id,
            Layer = door.Layer,
            Position = converter.Convert<Point, XmlCoord>(door.Position),
            Visible = true,
            // Visible is false iff there is an `<object>` or `<door>` XML element alongside this door 
            // and it's layer and position values are the same, also it's name is the same with a postfix "_dummy"
            // -> set it in RoomsConverter, and the default is true
        };
    }
}
