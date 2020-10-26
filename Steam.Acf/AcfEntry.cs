using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Steam.Acf
{
    /// <summary>
    /// Represents an element in an ACF file, that's either a compound element (<see cref="AcfObject"/>), 
    /// or a primitive (<see cref="AcfString"/>).
    /// </summary>
    public abstract class AcfEntry : IReadOnlyDictionary<string, AcfEntry>
    {
        /// <summary>
        /// Accesses the string value, if this is an <see cref="AcfString"/>.
        /// </summary>
        public abstract string Value { get; }

        // For AcfObject

        public abstract int Count { get; }
        public abstract AcfEntry this[string key] { get; }
        public abstract IEnumerable<string> Keys { get; }
        public abstract IEnumerable<AcfEntry> Values { get; }

        public abstract bool ContainsKey(string key);
        public abstract IEnumerator<KeyValuePair<string, AcfEntry>> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract bool TryGetValue(string key, out AcfEntry value);
    }
}
