using System;
using System.Collections.Generic;
using System.Text;

namespace Format.Xml.Helpers
{
    /// <summary>
    /// A type-safe wrapper over <see cref="IValueSerializer"/>.
    /// </summary>
    /// <typeparam name="T">The type handled by this serializer.</typeparam>
    public abstract class TypedValueSerializer<T> : IValueSerializer
    {
        /// <summary>
        /// Same as <see cref="Serialize(object)"/>, but with a type-safe interface.
        /// </summary>
        public abstract string SerializeTyped(T value);
        /// <summary>
        /// Same as <see cref="Deserialize(string)"/>, but with a type-safe interface.
        /// </summary>
        public abstract T DeserializeTyped(string value);

        public string Serialize(object value)
        {
            if (!typeof(T).IsAssignableFrom(value.GetType()))
            {
                throw new ArgumentException($"The value is not serializable by a {typeof(T).Name} serializer!", nameof(value));
            }
            return SerializeTyped((T)value);
        }

        public object Deserialize(string value) => DeserializeTyped(value);
    }
}
