using Nfh.Domain.Models.InGame;
using Nfh.Services.Common;
using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Nfh.Services.ProjectServices.Xml.Converters.LevelDatas
{
    internal class LevelDataConverter : TypeConverterBase<Level, XmlLevelData>
    {
        private readonly IConverter converter;

        public LevelDataConverter(IConverter converter)
        {
            this.converter = converter;
        }

        public override Level ConvertToDomain(XmlLevelData levelData)
        {
            var levelRoot = levelData.LevelRoot;
            var level = new Level
            {
                Name = levelRoot.Name,
                AngryTime = levelRoot.AngryTime,
                Size = converter.Convert<XmlCoord, Size>(levelRoot.Size),
                Objects = levelRoot.Objects.Select(converter.Convert<XmlLevelObject, LevelObject>).ToDictionary(v => v.Id, v => v),
                Rooms = levelRoot.Rooms.Select(converter.Convert<XmlLevelRoom, Room>).ToDictionary(v => v.Id, v => v),
                // 'Meta' is connected later, the data is loaded from another file
            };

            // Append localization
            applyToAllLevelObjects(level, (lo) => appendLocalization(lo, lo.Id, levelData.StringsRoot));
            foreach (var room in level.Rooms.Values)
                appendLocalization(room, room.Id, levelData.StringsRoot);

            // Connect all the doors
            var doors = level.Rooms.Values
                .SelectMany(r => r.Objects.Values)
                .Where(o => o is Door)
                .Select(o => (Door)o)
                .ToList();

            foreach (var door in doors)
                connectDoors(door, doors, levelRoot);

            // Connect visuals to level objects
            var visuals = levelData.AnimsRoot.Objects.Select(converter.Convert<XmlAnimsObject, Visuals>).ToDictionary(v => v.Id);
            foreach (var visual in visuals.Values)
                appendOffsetToVisualsFrames(visual, levelData.GfxDataRoot);

            var objectsInObjectsRoot = levelData
                .ObjectsRoot.Objects
                .Cast<XmlObjectsBase>()
                .Concat(levelData.ObjectsRoot.Doors)
                .Concat(levelData.ObjectsRoot.Actors)
                .ToLastKeepDictionary(o => o.Name);
            applyToAllLevelObjects(level, lo => appendVisualsToLevelObject(lo, visuals, objectsInObjectsRoot));

            // Add hotspot to actors
            var xmlActors = levelData.ObjectsRoot.Actors.ToDictionary(a => a.Name);
            foreach (var actor in level.Rooms.Values.SelectMany(r => r.Objects.Values).Concat(level.Objects.Values).OfType<Actor>())
            {
                if (xmlActors.TryGetValue(actor.Id, out var xmlActor))
                {
                    actor.Hotspot = xmlActor.Hotspot == null ? new Point(0, 0) : converter.Convert<XmlCoord, Point>(xmlActor.Hotspot);
                }
            }


            // Connect InteractionSpots with LevelObjects
            applyToAllLevelObjects(level, lo => appendInteractionPointsToLevelObjects(lo, objectsInObjectsRoot));

            return level;
        }

        public override XmlLevelData ConvertToXml(Level domain)
        {
            throw new NotImplementedException();
        }

        private void applyToAllLevelObjects(Level level, Action<LevelObject> action)
        {
            foreach (var obj in level.Objects.Values)
                action(obj);

            foreach (var obj in level.Rooms.Values.SelectMany(r => r.Objects.Values))
                action(obj);
        }

        private void appendLocalization(ILocalizable localizable, string id, XmlStringsRoot stringsRoot)
        {
            foreach (var e in stringsRoot.Entries.Where(e => e.Name == id))
                localizable.Localization.Strings[e.Category] = e.Text;
        }

        private void connectDoors(Door door, IReadOnlyList<Door> others, XmlLevelRoot levelRoot)
        {
            // Find all "<neighbor>" which "doorin" maches door.Id, and take the first one since only one should be a match
            var neighbor = levelRoot.Rooms
                .SelectMany(r => r.Neighbors)
                .Where(n => n.DoorIn == door.Id)
                .FirstOrDefault();

            door.Exit = others.FirstOrDefault(d => d.Id == neighbor?.DoorOut);
        }

        private void appendOffsetToVisualsFrames(Visuals visuals, XmlGfxRoot gfxDataRoot)
        {
            var files = gfxDataRoot.Objects
                .FirstOrDefault(o => o.Name == visuals.Id)
                ?.Files
                .ToDictionary(f => f.Image);

            if (files is null) return;

            foreach (var animation in visuals.Animations.Values)
            {
                foreach (var frame in animation.Frames)
                {
                    if (string.IsNullOrEmpty(frame.ImagePath)) continue;

                    frame.ImageOffset = converter.Convert<XmlCoord, Point>(
                        files[frame.ImagePath].Offset);
                }
            }
        }

        private void appendVisualsToLevelObject(LevelObject levelObject, IReadOnlyDictionary<string, Visuals> visuals, IReadOnlyDictionary<string, XmlObjectsBase> objects)
        {
            if (!objects.TryGetValue(levelObject.Id, out var obj) || obj.Graphics is null)
                return;

            if (visuals.TryGetValue(obj.Graphics, out var v))
                levelObject.Visuals = v;
        }

        private void appendInteractionPointsToLevelObjects(LevelObject levelObject, IReadOnlyDictionary<string, XmlObjectsBase> objects)
        {
            if (!objects.TryGetValue(levelObject.Id, out var obj))
                return;

            levelObject.InteractionSpots = obj.Hotspots.ToDictionary(h => h.Name, h => converter.Convert<XmlCoord, Point>(h.Offset));
        }
    }
}
