using System;
using System.Collections.Generic;

namespace Nfh.Services.Common
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
    }
}
