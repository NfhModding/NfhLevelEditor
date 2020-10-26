using System;
using System.Collections.Generic;

namespace Steam.Acf
{
    /// <summary>
    /// Represents a compound ACF entry, that holds key-value pairs.
    /// </summary>
    public class AcfObject : AcfEntry
    {
        public override string Value => throw new InvalidOperationException("Value can be only accessed on an AcfString!");

        /// <summary>
        /// The name of this <see cref="AcfObject"/>.
        /// </summary>
        public string Name { get; }

        public override int Count => entries.Count;

        private IReadOnlyDictionary<string, AcfEntry> entries = new Dictionary<string, AcfEntry>();

        public override IEnumerable<string> Keys => entries.Keys;
        public override IEnumerable<AcfEntry> Values => entries.Values;

        public override AcfEntry this[string key] => entries[key];

        internal AcfObject(string name, IReadOnlyDictionary<string, AcfEntry> entries)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("The name of an AcfObject can not be an empty string!", nameof(name));
            }
            this.entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        public override bool ContainsKey(string key) => entries.ContainsKey(key);
        public override bool TryGetValue(string key, out AcfEntry value) => entries.TryGetValue(key, out value);
        public override IEnumerator<KeyValuePair<string, AcfEntry>> GetEnumerator() => entries.GetEnumerator();

        public override string ToString() => AcfFile.ToString(this);
    }
}
