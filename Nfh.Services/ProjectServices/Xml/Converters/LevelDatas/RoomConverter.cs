using Nfh.Domain.Models.InGame;
using Nfh.Services.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class RoomConverter : TypeConverterBase<Room, XmlLevelRoom>
    {
        private readonly IConverter converter;

        public RoomConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Room ConvertToDomain(XmlLevelRoom room) => new(room.Name)
        {
            Path = new[] { converter.Convert<XmlCoord, Point>(room.Path1), converter.Convert<XmlCoord, Point>(room.Path2), },
            Offset = converter.Convert<XmlCoord, Point>(room.Offset),
            Floors = converter.Convert<List<XmlLevelFloor>, List<Floor>>(room.Floors),
            Walls = converter.Convert<List<XmlLevelFloor>, List<Wall>>(room.Floors),
            Objects = Enumerable.Empty<LevelObject>()
                     .Concat(room.Objects.Select(converter.Convert<XmlLevelObject, LevelObject>))
                     .Concat(room.Actors.Select(converter.Convert<XmlLevelActor, Actor>))
                     .Concat(room.Doors.Select(converter.Convert<XmlLevelDoor, Door>))
                     .ToLastKeepDictionary(v => v.Id),
            // Connecting doors cannot happend here, because the other other it more than possible to be in another door -> connect them in leveldata
        };

        public override XmlLevelRoom ConvertToXml(Room room)
        {
            var doors = room.Objects.Values
                    .OfType<Door>()
                    .Select(converter.Convert<Door, XmlLevelDoor>)
                    .ToList();

            var xmlLevelRoom = new XmlLevelRoom()
            {
                Name = room.Id,
                Path1 = converter.Convert<Point, XmlCoord>(room.Path[0]),
                Path2 = converter.Convert<Point, XmlCoord>(room.Path[1]),
                Offset = converter.Convert<Point, XmlCoord>(room.Offset),
                Floors = Enumerable.Empty<XmlLevelFloor>()
                            .Concat(converter.Convert<List<Wall>, List<XmlLevelFloor>>(room.Walls.ToList()))
                            .Concat(converter.Convert<List<Floor>, List<XmlLevelFloor>>(room.Floors.ToList()))
                            .ToList(),
                Doors = room.Objects.Values
                    .OfType<Door>()
                    .Select(d => convertToDoor(d, room.Objects.Values.ToList()))
                    .ToList(),
                Neighbors = convertToNeighbors(room.Objects.Values.OfType<Door>().ToList()),
                Actors = room.Objects.Values
                    .OfType<Actor>()
                    .Select(converter.Convert<Actor, XmlLevelActor>)
                    .ToList(),
                Objects = room.Objects.Values
                    .Where(o => o.GetType() == typeof(LevelObject))
                    .Select(converter.Convert<LevelObject, XmlLevelObject>)
                    .ToList(),
            };

            return xmlLevelRoom;
        }

        /// <summary>
        /// The logic behind this is reasoned in <see cref="DoorConverter.ConvertToXml(Door)"/>
        /// </summary>
        private XmlLevelDoor convertToDoor(Door door, IReadOnlyCollection<LevelObject> levelObjectsInRoom)
        {
            var dummy = levelObjectsInRoom
                .Where(lo => lo.Layer == door.Layer && lo.Position == door.Position)
                .Where(lo => lo.Id.EndsWith("_dummy") && lo.Id.Substring(0, door.Id.Length) == door.Id)
                .FirstOrDefault();

            var xmlDoor = converter.Convert<Door, XmlLevelDoor>(door);
            xmlDoor.Visible = dummy is null;

            return xmlDoor;
        }

        private List<XmlLevelNeighbor> convertToNeighbors(List<Door> doors) => doors
            .Where(d => d.Exit is not null)
            .Select(d => new XmlLevelNeighbor
            {
                Name = d.Id.Split('/')[1],
                DoorIn = d.Id,
                DoorOut = d.Exit.Id,
                Cost = 1000, // It is always 1000
            })
            .ToList();
    }
}
