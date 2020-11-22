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
        public LevelData UnifyWithGeneric(LevelData generic, LevelData level)
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

        public LevelData SeperateFromGeneric(LevelData unified)
        {
            return new();
        }

        private static StringsRoot UnifyStrings(StringsRoot generic, StringsRoot level) => new()
        { 
            Entries = generic.Entries.Unify(level.Entries, s => (s.Name, s.Category)),
        };

        // ToDo: It could more advanced: if there are only animations only override that or concat, otherwise replace all
        private static AnimsRoot UnifyAnims(AnimsRoot generic, AnimsRoot level) => new()
        {
            Objects = generic.Objects.Unify(level.Objects, o => o.Name),
        };

        private static GfxDataRoot UnifyGfxData(GfxDataRoot generic, GfxDataRoot level) => new()
        {
            Objects = generic.Objects.Unify(level.Objects, o => o.Name),
        };

        // ToDo
        private static ObjectsRoot UnifyObjects(ObjectsRoot generic, ObjectsRoot level)
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
