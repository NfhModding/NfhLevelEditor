using Nfh.Services.ProjectServices.Xml.Models;
using Nfh.Services.ProjectServices.Xml.Models.Anims;
using Nfh.Services.ProjectServices.Xml.Models.GfxData;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Models.Strings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nfh.Services.ProjectServices
{
    internal class LevelDataUnifier : ILevelDataUnifier
    {
        // Idea: make it pure, do not modify generic 
        public XmlLevelData UnifyWithGeneric(XmlLevelData generic, XmlLevelData level)
        {
            return new()
            {
                LevelRoot = level.LevelRoot,
                StringsRoot = UnifyStrings(generic.StringsRoot, level.StringsRoot),
                AnimsRoot = UnifyAnims(generic.AnimsRoot, level.AnimsRoot),
                GfxDataRoot = UnifyGfxData(generic.GfxDataRoot, level.GfxDataRoot),
                ObjectsRoot = UnifyObjects(generic.ObjectsRoot, level.ObjectsRoot),
            };
        }

        public XmlLevelData SeperateFromGeneric(XmlLevelData unified)
        {
            return new();
        }

        private static XmlStringsRoot UnifyStrings(XmlStringsRoot generic, XmlStringsRoot level) => new()
        { 
            Entries = generic.Entries.Unify(level.Entries, s => (s.Name, s.Category)),
        };

        // ToDo: It could more advanced: if there are only animations only override that or concat, otherwise replace all
        private static XmlAnimsRoot UnifyAnims(XmlAnimsRoot generic, XmlAnimsRoot level) => new()
        {
            Objects = generic.Objects.Unify(level.Objects, o => o.Name),
        };

        private static XmlGfxRoot UnifyGfxData(XmlGfxRoot generic, XmlGfxRoot level) => new()
        {
            Objects = generic.Objects.Unify(level.Objects, o => o.Name),
        };

        // ToDo
        private static XmlObjectsRoot UnifyObjects(XmlObjectsRoot generic, XmlObjectsRoot level)
        {
            return new()
            {
                Actors = generic.Actors.Concat(level.Actors).ToList(),
                Doors = generic.Doors.Concat(level.Doors).ToList(),
                Icons = generic.Icons.Concat(level.Icons).ToList(),
                Objects = generic.Objects.Concat(level.Objects).ToList(),
                Inventars = generic.Inventars.Concat(level.Inventars).ToList(),
            };
        }
    }

    internal static class IEnumerableExtensions
    {
        public static List<TSource> Unify<TSource, TKey>(this IEnumerable<TSource> generic, IEnumerable<TSource> level, Func<TSource, TKey> keySelector)
        {
            var unified = generic.ToDictionary(keySelector, v => v);
            foreach (var item in level)
            {
                unified[keySelector(item)] = item;
            }
            return unified.Values.ToList();
        }
    }
}
