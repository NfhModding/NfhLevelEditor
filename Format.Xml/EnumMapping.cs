using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Format.Xml
{
    internal class EnumMapping
    {
        private Dictionary<object, string> valueToString = new Dictionary<object, string>();
        private Dictionary<string, object> stringToValue = new Dictionary<string, object>();

        public EnumMapping(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Expected an enum type!", nameof(enumType));
            }
            var enumNames = enumType.GetEnumNames();
            foreach (var enumName in enumNames)
            {
                var enumValue = Enum.Parse(enumType, enumName);
                var fieldInfo = enumType.GetField(enumName);
                var enumAttrib = fieldInfo.GetCustomAttribute<XmlEnumAttribute>();
                var mappedName = enumAttrib == null ? enumName : enumAttrib.Name;
                valueToString.Add(enumValue, mappedName);
                stringToValue.Add(mappedName, enumValue);
            }
        }
        public string ValueToName(object value) => valueToString[value];
        public object NameToValue(string name) => stringToValue[name];
    }
}
