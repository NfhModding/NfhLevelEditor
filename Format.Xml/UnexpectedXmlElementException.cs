using System;

namespace Format.Xml
{
    /// <summary>
    /// An error that happens during deserialization, when an attribute or subnode could
    /// not be matched to anything in the data model.
    /// </summary>
    public class UnexpectedXmlElementException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="UnexpectedXmlElementException"/>.
        /// </summary>
        /// <param name="elementName">Thename of the unexpected element.</param>
        /// <param name="elementKind">The kind of the element, like attribut or subnode.</param>
        /// <param name="type">The type that was being deserialized.</param>
        internal UnexpectedXmlElementException(string elementName, string elementKind, Type type)
            : base($"The {elementKind} '{elementName}' was not expected while deserializing the type '{type.Name}'!")
        {
        }
    }
}
