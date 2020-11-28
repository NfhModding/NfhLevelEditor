using Nfh.Services.Helpers;
using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;
using System.Linq;

namespace Nfh.Services.ProjectServices
{
    internal class LevelDataUnifier : ILevelDataUnifier
    {
        public XmlLevelData UnifyWithGeneric(XmlLevelData generic, XmlLevelData level) => new()
        {
            LevelRoot = level.LevelRoot,
            StringsRoot = UnifyStrings(generic.StringsRoot, level.StringsRoot),
            AnimsRoot = UnifyAnims(generic.AnimsRoot, level.AnimsRoot),
            GfxDataRoot = UnifyGfxData(generic.GfxDataRoot, level.GfxDataRoot),
            ObjectsRoot = UnifyObjects(generic.ObjectsRoot, level.ObjectsRoot),
        };

        public XmlLevelData SeperateFromGeneric(XmlLevelData generic, XmlLevelData unified) => new()
        {
            LevelRoot = unified.LevelRoot,
            StringsRoot = SeperateStrings(generic.StringsRoot, unified.StringsRoot),
        };


        private static XmlStringsRoot UnifyStrings(XmlStringsRoot generic, XmlStringsRoot level) => new()
        {
            Entries = generic.Entries.SetOrOverride(level.Entries, s => (s.Name, s.Category)),
        };

        private static XmlStringsRoot SeperateStrings(XmlStringsRoot generic, XmlStringsRoot unified)
        {
            var genericEntries = generic.Entries.ToDictionary(s => (s.Name, s.Category));

            var root = new XmlStringsRoot();
            foreach (var entry in unified.Entries)
            {
                if (!genericEntries.TryGetValue((Name: entry.Name, Category: entry.Category), out var _))
                {
                    root.Entries.Add(entry);
                }
            }

            return root;
        }

        
        private static XmlAnimsRoot UnifyAnims(XmlAnimsRoot generic, XmlAnimsRoot level)
        {
            var unified = generic.Objects.ToDictionary(g => g.Name);
            foreach (var obj in level.Objects)
            {
                if (unified.TryGetValue(obj.Name, out var animsObj))
                {
                    animsObj.Animations.AddRange(obj.Animations);
                    animsObj.Regions.AddRange(obj.Regions);
                }
                else
                {
                    unified[obj.Name] = obj;
                }
            }

            return new()
            {
                Objects = unified.Values.ToList(),
            };
        }

        private static XmlGfxRoot UnifyGfxData(XmlGfxRoot generic, XmlGfxRoot level)
        {
            var unified = generic.Objects.ToDictionary(o => o.Name);
            foreach (var obj in level.Objects)
            {
                if (unified.TryGetValue(obj.Name, out var gfxObj))
                {
                    gfxObj.Files.AddRange(obj.Files);
                }
                else
                {
                    unified[obj.Name] = obj;
                }
            }

            return new()
            {
                Objects = unified.Values.ToList(),
            };
        }

        private static XmlObjectsRoot UnifyObjects(XmlObjectsRoot generic, XmlObjectsRoot level)
        {
            return new()
            {
                Actors = generic.Actors.SetOrOverride(level.Actors, o => o.Name),
                Doors = generic.Doors.SetOrOverride(level.Doors, o => o.Name),
                Icons = generic.Icons.SetOrOverride(level.Icons, o => o.Name),
                Objects = generic.Objects.SetOrOverride(level.Objects, o => o.Name),
                Inventars = generic.Inventars.SetOrOverride(level.Inventars, o => o.Name),
            };
        }
    }
}
