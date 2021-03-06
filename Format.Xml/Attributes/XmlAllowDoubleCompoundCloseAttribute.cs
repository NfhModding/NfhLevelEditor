﻿using System;

namespace Format.Xml.Attributes
{
    /// <summary>
    /// Allows the compound tag close sequence to be repeated. Basically allows accidental '/>'.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class XmlAllowDoubleCompoundCloseAttribute : Attribute
    {
    }
}
