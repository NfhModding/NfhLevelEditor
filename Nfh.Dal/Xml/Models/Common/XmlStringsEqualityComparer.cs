using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nfh.Dal.Xml.Models.Common
{
    internal class XmlStringsEqualityComparer : IEqualityComparer<XmlString>
    {
        public bool Equals(XmlString? x, XmlString? y) =>
            x?.Name == y?.Name && x?.Category == y?.Category;

        public int GetHashCode([DisallowNull] XmlString obj) =>
            HashCode.Combine(obj.Name, obj.Category);
    }
}
