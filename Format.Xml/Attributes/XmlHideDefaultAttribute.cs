using System;

namespace Format.Xml.Attributes
{
    /// <summary>
    /// An attribute to denote that an XML value doesn't have to be serialized if it's
    /// some default value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class XmlHideDefaultAttribute : Attribute
    {
        /// <summary>
        /// A tag-value for the case that the user didn't specify a default.
        /// </summary>
        internal static readonly object Unspecified = new object();

        /// <summary>
        /// The default value that doesn't have to be serialized.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Creates a <see cref="XmlHideDefaultAttribute"/> with the given default.
        /// </summary>
        /// <param name="value">The default value that doesn't have to be serialized.</param>
        public XmlHideDefaultAttribute(object value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a <see cref="XmlHideDefaultAttribute"/> with the type-based default value.
        /// </summary>
        public XmlHideDefaultAttribute()
            : this(Unspecified)
        {
        }
    }
}
