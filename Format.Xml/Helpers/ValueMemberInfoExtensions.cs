using System;
using System.Linq;
using System.Reflection;

namespace Format.Xml.Helpers
{
    internal static class ValueMemberInfoExtensions
    {
        public static ValueMemberInfo[] GetValueMembers(this Type type, BindingFlags flags)
        {
            return type.GetMembers(flags)
                .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property)
                .Select(x => x.MemberType == MemberTypes.Field
                           ? new ValueMemberInfo(x as FieldInfo)
                           : new ValueMemberInfo(x as PropertyInfo))
                .ToArray();
        }
    }
}
