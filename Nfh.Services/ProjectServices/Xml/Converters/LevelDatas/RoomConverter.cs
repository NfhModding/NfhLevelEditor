using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class RoomConverter : TypeConverterBase<Room, XmlLevelRoom>
    {
        private readonly IConverter converter;

        public RoomConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Room convertToDomain(XmlLevelRoom room) => new(room.Name)
        {
            // Path = ... Only has a getter
            Offset = converter.Convert<XmlCoord, Point>(room.Offset),
            Floors = converter.Convert<List<XmlLevelFloor>, List<Floor>>(room.Floors),
            Walls = converter.Convert<List<XmlLevelFloor>, List<Wall>>(room.Floors),
            Objects = Enumerable.Empty<LevelObject>()
                     .Concat(room.Objects.Select(converter.Convert<XmlLevelObject, LevelObject>))
                     .Concat(room.Actors.Select(converter.Convert<XmlLevelActor, Actor>))
                     .Concat(room.Doors.Select(converter.Convert<XmlLevelDoor, Door>))
                     .ToDictionary(v => v.Id, v => v),
        };

        public override XmlLevelRoom convertToXml(Room domain)
        {
            throw new NotImplementedException();
        }
    }
}
