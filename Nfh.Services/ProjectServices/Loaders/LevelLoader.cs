﻿using Nfh.Domain.Models.InGame;
using Nfh.Services.ProjectServices.Xml.Converters;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Level;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Nfh.Services.ProjectServices.Loaders
{
    internal class LevelLoader : ILevelLoader
    {
        private readonly ILevelDataLoader levelDataLoader;
        //private readonly ILevelMetaLoader levelMetaLoader;
        private readonly ILevelDataUnifier levelDataUnifier;
        private readonly Converter converter;

        public LevelLoader(
            ILevelDataLoader levelDataLoader,
            //ILevelMetaLoader levelMetaLoader,
            ILevelDataUnifier levelDataUnifier,
            Converter converter)
        {
            this.levelDataLoader = levelDataLoader;
           // this.levelMetaLoader = levelMetaLoader;
            this.levelDataUnifier = levelDataUnifier;
            this.converter = converter;
        }

        public Level Load(DirectoryInfo gamedataFolder, string levelId)
        {
            var levelData = levelDataUnifier.UnifyWithGeneric(
                generic: levelDataLoader.LoadGenericData(gamedataFolder), 
                level: levelDataLoader.LoadLevelSpecificData(gamedataFolder, levelId));

            // var level = converter.Convert<LevelData, Level>(levelData);
            // level.Meta = levelMetaLoader.Load(gamedataFolder, levelId);

            var levelRoot = levelData.LevelRoot;
            var level = new Level
            {
                Name = levelRoot.Name,
                AngryTime = levelRoot.AngryTime,
                Size = converter.Convert<Coord, Size>(levelRoot.Size),
                Meta = new(""), // ToDo get from briefings
                Objects = levelRoot.Objects.Select(convertObjectToLevelObjects).ToDictionary(v => v.Id, v => v),
                Rooms = levelRoot.Rooms.Select(convertToRoom).ToDictionary(v => v.Id, v => v),
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
            var visuals = levelData.AnimsRoot.Objects.Select(convertToVisuals).ToDictionary(v => v.Id);
            foreach (var visual in visuals.Values)            
                appendOffsetToVisualsFrames(visual, levelData.GfxDataRoot);

            var objectsInObjectsRoot = levelData.ObjectsRoot.Objects.ToDictionary(o => o.Name);
            applyToAllLevelObjects(level, lo => appendVisualsToLevelObject(lo, visuals, objectsInObjectsRoot));

            // ToDo Connect InteractionSpots with LevelObjects

            return level;
        }

        private void applyToAllLevelObjects(Level level, Action<LevelObject> action)
        {
            foreach (var obj in level.Objects.Values)
                action(obj);

            foreach (var obj in level.Rooms.Values.SelectMany(r => r.Objects.Values))
                action(obj);
        }

        private void appendOffsetToVisualsFrames(Visuals visuals, GfxDataRoot gfxDataRoot)
        {
            var files = gfxDataRoot.Objects
                .FirstOrDefault(o => o.Name == visuals.Id)
                ?.Files
                .ToDictionary(f => f.Image);

            if (files is null)
                throw new("AnimObject is not found in gfxdata");

            foreach (var animation in visuals.Animations.Values)
            {
                foreach (var frame in animation.Frames)
                {
                    frame.ImageOffset = converter.Convert<Coord, Point>(
                        files[frame.ImagePath].Offset);
                }
            }            
        }

        private void appendVisualsToLevelObject(LevelObject levelObject, IReadOnlyDictionary<string, Visuals> visuals, IReadOnlyDictionary<string, XmlObjectsObject> objects)
        {
            if (!objects.TryGetValue(levelObject.Id, out var obj))
                return;

            if (visuals.TryGetValue(obj.Graphics, out var v))
                levelObject.Visuals = v;
        }


        private void connectDoors(Door door, IReadOnlyList<Door> others, LevelRoot levelRoot)
        {
            // Find all "<neighbor>" which "doorin" maches door.Id, and take the first one since only one should be a match
            var neighbor = levelRoot.Rooms
                .SelectMany(r => r.Neighbors)
                .Where(n => n.DoorIn == door.Id)
                .FirstOrDefault();

            door.Exit = others.FirstOrDefault(d => d.Id == neighbor?.DoorOut);
        }

        // Call this for all child
        private void appendLocalization(ILocalizable localizable, string id, StringsRoot stringsRoot)
        {
            foreach (var e in stringsRoot.Entries.Where(e => e.Name == id))
                localizable.Localization.Strings[e.Category] = e.Text;
        }

        public void Save(DirectoryInfo gamedataFolder, Level level)
        {
            throw new NotImplementedException();
        }

        // ---- Visuals

        private VisualRegion convertToRegion(XmlRegion region)
        {
            return new()
            {
                Bounds = new()
                {
                    Size = converter.Convert<Coord, Size>(region.Size),
                    Location = convertCoordToPoint(region.Position),
                },
                Text = region.Type == XmlRegion.RegionType.Text,
            };
        }

        private Animation.Frame convertToFrame(XmlFrame frame)
        {
            return new()
            {
                ImagePath = frame.Graphics,
                SoundPath = frame.Sound,
                // ImageOffset, later (need gfxdata)
            };
        }

        private Animation.Kind convetToKind(XmlAnimation.Kind animType)
        {
            return animType switch
            {
                XmlAnimation.Kind.OneShot => Animation.Kind.SingleShot,
                XmlAnimation.Kind.Loop => Animation.Kind.Loop,
                _ => throw new NotImplementedException(),
            };
        }

        private Animation converToAnimation(XmlAnimation animation)
        {
            return new(animation.Name)
            {
                Frames = animation.Frames.Select(convertToFrame).ToList(),
                Kind_ = convetToKind(animation.Type),
            };
        }

        private Visuals convertToVisuals(AnimObject animObject)
        {
            return new(animObject.Name)
            {
                Regions = animObject.Regions.Select(convertToRegion).ToList(),
                Animations = animObject.Animations.Select(converToAnimation).ToDictionary(v => v.Id, v => v),
            };
        }

        // ----

        private LevelObject convertObjectToLevelObjects(XmlLevelObject obj)
        {
            return new(obj.Name)
            {
                Layer = obj.Layer,
                Position = convertCoordToPoint(obj.Position),
                // The rest is set later
            };
        }

        private Room convertToRoom(LevelRoom room)
        {
            return new(room.Name)
            {
                // Path = ... Only has a getter
                Offset = convertCoordToPoint(room.Offset),
                Floors = convertFloorsToFloors(room.Floors),
                Walls = convertFloorsToWalls(room.Floors),
                Objects = Enumerable.Empty<LevelObject>()
                    .Concat(room.Objects.Select(convertObjectToLevelObjects))
                    .Concat(room.Actors.Select(convertToActor))
                    .Concat(room.Doors.Select(convertToDoor))
                    .ToDictionary(v => v.Id, v => v),
            };
        }

        private Actor convertToActor(LevelActor xml)
        {
            return new(xml.Name)
            {
                Layer = xml.Layer,
                Position = convertCoordToPoint(xml.Position),
                // The rest is set later
            };
        }

        private Door convertToDoor(LevelDoor xml)
        {
            return new(xml.Name)
            {
                Layer = xml.Layer,
                Position = convertCoordToPoint(xml.Position),
                // The rest is set later
            };
        }

        private IList<Wall> convertFloorsToWalls(List<LevelFloor> floors)
        {
            return floors
                .Where(f => f.Wall)
                .Select(f => new Wall
                {
                    Bounds = new Rectangle() { Size = convertCoordToSize(f.Size), Location = convertCoordToPoint(f.Offset) },
                })
                .ToList();
        }

        private IList<Floor> convertFloorsToFloors(List<LevelFloor> floors)
        {
            return floors
                .Where(f => !f.Wall)
                .Select(f => new Floor
                {
                    Bounds = new Rectangle() { Size = convertCoordToSize(f.Size), Location = convertCoordToPoint(f.Offset) },
                })
                .ToList();
        }

        private Point convertCoordToPoint(Coord? coord)
        {
            // ToDo convert back -> null iff object.name = house
            if (coord is null)
                return new(0, 0);

            return new()
            {
                X = coord.X,
                Y = coord.Y,
            };
        }

        private Size convertCoordToSize(Coord coord)
        {
            // ToDo check
            return new()
            {
                Width = coord.X,
                Height = coord.Y,
            };
        }
    }
}
