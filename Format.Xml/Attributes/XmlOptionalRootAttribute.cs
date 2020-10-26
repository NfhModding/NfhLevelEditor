using System;
using System.Collections.Generic;
using System.Text;

namespace Format.Xml.Attributes
{
    /// <summary>
    /// Used to denote an optional root element, that might not be there when deserializing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class XmlOptionalRootAttribute : Attribute
    {
        /// <summary>
        /// The name of the optional root tag.
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Creates a new <see cref="XmlOptionalRootAttribute"/> with the given root name.
        /// </summary>
        /// <param name="name">The root name.</param>
        public XmlOptionalRootAttribute(string name)
        {
            ElementName = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Creates a new <see cref="XmlOptionalRootAttribute"/> with the default root name.
        /// </summary>
        public XmlOptionalRootAttribute()
            : this(string.Empty)
        {
        }
    }
}
