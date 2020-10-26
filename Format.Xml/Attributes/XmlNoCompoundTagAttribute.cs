using System;
using System.Collections.Generic;
using System.Text;

namespace Format.Xml.Attributes
{
    /// <summary>
    /// Disables compound closing of the tag, so even if the tag contains nothing,
    /// it will have a separate ending tag instead of a compound closing tag.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class XmlNoCompoundTagAttribute : Attribute
    {
    }
}
