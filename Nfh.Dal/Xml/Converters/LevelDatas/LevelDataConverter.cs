using Nfh.Domain.Models.InGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Nfh.Dal.Xml.Models;
using Nfh.Dal.Xml.Models.Anims;
using Nfh.Dal.Xml.Models.Common;
using Nfh.Dal.Xml.Models.GfxData;
using Nfh.Dal.Xml.Models.Level;
using Nfh.Dal.Xml.Models.Objects;
using Nfh.Dal.Xml.Models.Strings;
using Nfh.Dal.Helpers;

namespace Nfh.Dal.Xml.Converters.LevelDatas
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
            var strings = levelData.StringsRoot.Entries.Select(e => (xmlString: e, hasRead: false)).ToList();

            applyToAllLevelObjects(level, (lo) => appendLocalization(lo, lo.Id, strings));
            foreach (var room in level.Rooms.Values)
                appendLocalization(room, room.Id, strings);

            foreach (var (str, _) in strings.Where(s => !s.hasRead).ToList())
            {
                if (level.ObjectDependentLocalizations.TryGetValue(str.Name, out var localization))
                {
                    localization.Strings.Add(str.Category, str.Text);
                }
                else
                {
                    level.ObjectDependentLocalizations[str.Name] = new Localization { Strings = new Dictionary<string, string>() { [str.Category] = str.Text } };
                }
            }

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

        public override XmlLevelData ConvertToXml(Level level)
        {
            // LevelRoot
            var levelRoot = new XmlLevelRoot
            {
                Name = level.Name,
                AngryTime = level.AngryTime,
                Size = converter.Convert<Size, XmlCoord>(level.Size),
                Objects = level.Objects.Values.Select(converter.Convert<LevelObject, XmlLevelObject>).ToList(),
                Rooms = level.Rooms.Values.Select(converter.Convert<Room, XmlLevelRoom>).ToList(),
            };

            // StringRoot
            var entries = new List<XmlString>();
            var allLocalizationString = getFromAllLevelObject(level, lo => (id: lo.Id, strings: lo.Localization.Strings))
                .Concat(level.ObjectDependentLocalizations.Select(pair => (id: pair.Key, strings: pair.Value.Strings)));
            foreach (var loc in allLocalizationString)
            {
                foreach (var str in loc.strings)
                {
                    entries.Add(new()
                    {
                        Name = loc.id,
                        Category = str.Key,
                        Text = str.Value,
                    });
                }
            }
            var stringsRoot = new XmlStringsRoot
            {
                Entries = entries.Distinct(new XmlStringsEqualityComparer()).ToList(),
            };

            return new()
            {
                LevelRoot = levelRoot,
                StringsRoot = stringsRoot,
                // Right now the UI only changes the above mentiont things
            };
        }

        private List<TReturn> getFromAllLevelObject<TReturn>(Level level, Func<LevelObject, TReturn> getFunc)
        {
            var result = new List<TReturn>();
            foreach (var obj in level.Objects.Values)
                result.Add(getFunc(obj));

            foreach (var obj in level.Rooms.Values.SelectMany(r => r.Objects.Values))
                result.Add(getFunc(obj));

            return result;
        }

        private void applyToAllLevelObjects(Level level, Action<LevelObject> action)
        {
            foreach (var obj in level.Objects.Values)
                action(obj);

            foreach (var obj in level.Rooms.Values.SelectMany(r => r.Objects.Values))
                action(obj);
        }

        private void appendLocalization(ILocalizable localizable, string id, List<(XmlString xmlString, bool hasRead)> strings)
        {
            var currentStrings = strings.Where(e => e.xmlString.Name == id).ToList();
            for (var i = 0; i < currentStrings.Count; i++)
            {
                var str = currentStrings[i];
                localizable.Localization.Strings[str.xmlString.Category] = str.xmlString.Text;
                str.hasRead = true;
            }   
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

            if (files is null)
                return;

            foreach (var animation in visuals.Animations.Values)
            {
                foreach (var frame in animation.Frames)
                {
                    if (string.IsNullOrEmpty(frame.ImagePath))
                        continue;

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
