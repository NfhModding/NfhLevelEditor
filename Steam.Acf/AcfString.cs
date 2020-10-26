using System;
using System.Collections.Generic;

namespace Steam.Acf
{
    /// <summary>
    /// A primitive <see cref="AcfEntry"/>, basically a string value.
    /// </summary>
    public class AcfString : AcfEntry
    {
        public override string Value { get; }

        public override int Count => throw new NotSupportedException("Count is only supported for AcfObject!");
        public override AcfEntry this[string key] => throw new NotSupportedException("Indexing is only supported for AcfObject!");
        public override IEnumerable<string> Keys => throw new NotSupportedException();
        public override IEnumerable<AcfEntry> Values => throw new NotSupportedException();

        internal AcfString(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override bool ContainsKey(string key) =>
            throw new InvalidOperationException("ContainsKey is only supported for AcfObject!");
        public override bool TryGetValue(string key, out AcfEntry value) =>
            throw new InvalidOperationException("TryGetValue is only supported for AcfObject!");
        public override IEnumerator<KeyValuePair<string, AcfEntry>> GetEnumerator() =>
            throw new InvalidOperationException("GetEnumerator is only supported for AcfObject!");

        public override string ToString() => Value;
    }
}
