namespace Format.Xml
{
    /// <summary>
    /// The value serializer used by <see cref="XmlSerializer"/>.
    /// </summary>
    public interface IValueSerializer
    {
        /// <summary>
        /// Serializes the given object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>The string representation of the object.</returns>
        string Serialize(object value);
        /// <summary>
        /// Deserializes an object from a string.
        /// </summary>
        /// <param name="value">The string to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        object Deserialize(string value);
    }
}
