using System;
using System.Reflection;

namespace Format.Xml.Helpers
{
    internal class ValueMemberInfo
    {
        private MemberInfo info;

        public string Name => info.Name;
        public Type FieldType => info.MemberType == MemberTypes.Field
                               ? (info as FieldInfo).FieldType
                               : (info as PropertyInfo).PropertyType;

        public ValueMemberInfo(FieldInfo fieldInfo)
        {
            info = fieldInfo ?? throw new ArgumentNullException(nameof(fieldInfo));
        }

        public ValueMemberInfo(PropertyInfo propertyInfo)
        {
            info = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public bool HasAttribute<T>() where T : Attribute => info.IsDefined(typeof(T));
        public T GetAttribute<T>() where T : Attribute => info.GetCustomAttribute<T>();

        public void SetValue(object target, object value)
        {
            if (info.MemberType == MemberTypes.Field)
            {
                (info as FieldInfo).SetValue(target, value);
            }
            else
            {
                (info as PropertyInfo).SetValue(target, value);
            }
        }

        public object GetValue(object target) => info.MemberType == MemberTypes.Field
             ? (info as FieldInfo).GetValue(target)
             : (info as PropertyInfo).GetValue(target);
    }
}
