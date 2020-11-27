using System;
using System.Collections.Generic;
using System.Linq;

namespace Nfh.Services.Helpers
{
    internal static class LinqExtensions
    {
        internal static Dictionary<TKey, TSource> ToLastKeepDictionary<TKey, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
        {
            var result = new Dictionary<TKey, TSource>();
            foreach (var element in source) result[keySelector(element)] = element;
            return result;
        }

        public static List<TSource> SetOrOverride<TSource, TKey>(this IEnumerable<TSource> generic, IEnumerable<TSource> level, Func<TSource, TKey> keySelector)
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
