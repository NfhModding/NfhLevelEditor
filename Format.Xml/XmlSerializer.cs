using Format.Xml.Attributes;
using Format.Xml.Helpers;
using Format.Xml.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Format.Xml
{
    /**
     * Sadly the game's XML files are not always valid XML. Also, the standard XML tooling
     * is missing a few features, that would make the job a lot simpler (easier control over
     * attribute specification, custom attribute value converters, ...). An alternative,
     * ExtendedXmlSerializer fixed all the missing features, but invalid XML is still a pain,
     * and the library is the slowest thing I've ever seen.
     * 
     * And so I've decided to write a pseudo-solution, that will make-do for just this software.
     */

    /// <summary>
    /// Handles a pseudo-XML serializarion and deserialization process.
    /// </summary>
    public class XmlSerializer
    {
        private Dictionary<Type, IValueSerializer> serializers = new Dictionary<Type, IValueSerializer>();
        private Dictionary<Type, EnumMapping> enumMappings = new Dictionary<Type, EnumMapping>();
        private XmlBuilder serBuilder = new XmlBuilder();
        private ParseState deState;

        /// <summary>
        /// Returns an <see cref="XmlSerializer"/> with all of the builtin serializers registered.
        /// </summary>
        /// <returns>An <see cref="XmlSerializer"/> with defaul serializers.</returns>
        public static XmlSerializer WithDefaultSerializers()
        {
            var serializer = new XmlSerializer();

            // string
            serializer.RegisterValue(x => x, x => x);
            // int
            serializer.RegisterValue(x => x.ToString(), x => int.Parse(x));
            // bool
            serializer.RegisterValue(x => x.ToString().ToLower(), x => bool.Parse(x));

            return serializer;
        }

        /// <summary>
        /// Registers a new serializer for a given type.
        /// </summary>
        /// <param name="type">The type to register a serializer for.</param>
        /// <param name="serializer">The serializer to register for the type.</param>
        public void RegisterValue(Type type, IValueSerializer serializer)
        {
            serializers.Add(type, serializer);
        }

        /// <summary>
        /// Registers a new serializer for a given type.
        /// </summary>
        /// <typeparam name="T">The type to register a serializer for.</typeparam>
        /// <param name="ser">The serializer function.</param>
        /// <param name="de">The deserializer function.</param>
        public void RegisterValue<T>(Func<T, string> ser, Func<string, T> de)
        {
            RegisterValue(typeof(T), new LambdaValueSerializer<T> { Serializer = ser, Deserializer = de });
        }

        /// <summary>
        /// Serializes the given object into XML.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized XML string.</returns>
        public string Serialize(object obj)
        {
            serBuilder.Clear();
            var rootName = RootNodeName(obj.GetType(), out var _);
            if (rootName != null)
            {
                SerializeNode(null, rootName, obj);
            }
            else
            {
                SerializeContent(obj);
            }
            return serBuilder.ToString();
        }

        private void SerializeNode(ValueMemberInfo memInfo, string name, object value)
        {
            if (SerializeText(memInfo, value, out var text))
            {
                // We could serialize it as text
                if (name == null || text == null)
                {
                    // Without a name we can't insert it
                    // Also if text is null, it's a default
                    return;
                }
                serBuilder.AddTextNode(name, text);
                return;
            }
            // It's a compound type
            serBuilder.PushNode(name);
            // Check if we need to disable compound closing
            // Either on the member info, or on the type
            var disableAttribOnMember = memInfo?.GetAttribute<XmlNoCompoundTagAttribute>();
            var disableAttribOnType = value.GetType().GetCustomAttribute<XmlNoCompoundTagAttribute>();
            if (disableAttribOnMember != null || disableAttribOnType != null)
            {
                serBuilder.DisableCompoundClose();
            }
            SerializeContent(value);
            serBuilder.PopNode();
        }

        private bool SerializeText(ValueMemberInfo memInfo, object value, out string xml)
        {
            // First check if it can be hidden
            var hideAttrib = memInfo?.GetAttribute<XmlHideDefaultAttribute>();
            if (hideAttrib != null)
            {
                var defaultValue = hideAttrib.Value;
                if (defaultValue == XmlHideDefaultAttribute.Unspecified)
                {
                    defaultValue = DefaultConstruct(value.GetType());
                }
                if (value.Equals(defaultValue))
                {
                    // It's a default and we want to hide it
                    xml = null;
                    return true;
                }
            }
            var valueType = value.GetType();
            if (serializers.TryGetValue(valueType, out var serializer))
            {
                // We have a serializer for this type, use that
                xml = serializer.Serialize(value);
                return true;
            }
            if (valueType.IsEnum)
            {
                // It's an enum, use the mapping
                var enumMapping = GetEnumMapping(valueType);
                xml = enumMapping.ValueToName(value);
                return true;
            }
            // Not valid as text
            xml = null;
            return false;
        }

        private void SerializeContent(object value)
        {
            // We collect each member without the XmlIgnore attribute
            var valueType = value.GetType();
            var memInfos = GetXmlMembers(valueType);
            // Now serialize each member
            foreach (var memInfo in memInfos)
            {
                var memValue = memInfo.GetValue(value);
                if (memValue == null)
                {
                    // We leave out null values
                    continue;
                }

                var attribName = AttributeName(memInfo);
                if (attribName != null)
                {
                    // Just an attribute
                    if (!SerializeText(memInfo, memValue, out var xml))
                    {
                        throw new InvalidOperationException("Can't serialize attribute!");
                    }
                    if (xml != null)
                    {
                        // Not defaulted
                        serBuilder.AddAttribute(attribName, xml);
                    }
                }
                else if (ListName(memInfo, out var listName, out var listElementName))
                {
                    // A list, either nested, or inline
                    var list = memValue as IList;
                    if (listName != null)
                    {
                        serBuilder.PushNode(listName);
                        // Check if we need to disable compound nodes
                        var disableAttrib = memInfo.GetAttribute<XmlNoCompoundTagAttribute>();
                        if (disableAttrib != null)
                        {
                            serBuilder.DisableCompoundClose();
                        }
                    }
                    foreach (var element in list)
                    {
                        SerializeNode(null, listElementName, element);
                    }
                    if (listName != null)
                    {
                        serBuilder.PopNode();
                    }
                }
                else
                {
                    // Just an element
                    var elementName = ElementName(memInfo);
                    SerializeNode(memInfo, elementName, memValue);
                }
            }
        }

        /// <summary>
        /// Deserializes the given XML string into a new object.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize.</typeparam>
        /// <param name="value">The XML string to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        public T Deserialize<T>(string value)
        {
            deState = new ParseState(value);
            if (!XmlParser.MatchXmlHeader(ref deState))
            {
                ThrowSyntaxError("Missing valid XML header!");
            }
            var objType = typeof(T);
            var rootName = RootNodeName(objType, out var optional);
            object obj;
            if (rootName != null)
            {
                if (optional)
                {
                    if (XmlParser.MatchOpenTagBegin(ref deState, rootName))
                    {
                        // Had a root node
                        obj = DeserializeNode(null, rootName, objType);
                    }
                    else
                    {
                        // Was no root node
                        obj = DefaultConstruct(objType);
                        DeserializeSubnodes(obj);
                    }
                }
                else
                {
                    // Must have a root
                    if (!XmlParser.MatchOpenTagBegin(ref deState, rootName))
                    {
                        ThrowSyntaxError("Missing root element open tag!");
                    }
                    obj = DeserializeNode(null, rootName, objType);
                }
            }
            else
            {
                // No root needed
                obj = DefaultConstruct(objType);
                DeserializeSubnodes(obj);
            }
            return (T)obj;
        }

        private object DeserializeNode(ValueMemberInfo memInfo, string tag, Type objTy)
        {
            if (serializers.TryGetValue(objTy, out var serializer))
            {
                // We have a serializer
                // Close the current tag
                if (!XmlParser.MatchOpenTagEnd(ref deState))
                {
                    ThrowSyntaxError("Missing '>'!");
                }
                // Parse out content
                XmlParser.MatchTagContent(ref deState, out var content);
                // Close the opened tag
                if (!XmlParser.MatchCloseTag(ref deState, tag))
                {
                    ThrowSyntaxError($"Missing close tag for '{tag}'!");
                }
                // Deserialize the content
                return serializer.Deserialize(content);
            }
            else if (objTy.IsEnum)
            {
                // Enum
                // Close the current tag
                if (!XmlParser.MatchOpenTagEnd(ref deState))
                {
                    ThrowSyntaxError("Missing '>'!");
                }
                // Parse out content
                XmlParser.MatchTagContent(ref deState, out var content);
                // Close the opened tag
                if (!XmlParser.MatchCloseTag(ref deState, tag))
                {
                    ThrowSyntaxError($"Missing close tag for '{tag}'!");
                }
                // Get the enum mapping, read out value
                var enumMapping = GetEnumMapping(objTy);
                return enumMapping.NameToValue(content);
            }
            else
            {
                // Compound type
                var obj = DefaultConstruct(objTy);
                // We are past the name here, we need attributes and sub-content
                DeserializeAttributes(obj);
                if (XmlParser.MatchOpenTagCompoundEnd(ref deState))
                {
                    // Check if we could consume one more close tag
                    var objDoubleCloseAttrib = objTy.GetCustomAttribute<XmlAllowDoubleCompoundCloseAttribute>();
                    var memDoubleCloseAttrib = memInfo?.GetAttribute<XmlAllowDoubleCompoundCloseAttribute>();
                    if (objDoubleCloseAttrib != null || memDoubleCloseAttrib != null)
                    {
                        XmlParser.MatchOpenTagCompoundEnd(ref deState);
                    }
                    // Do nothing, we are done
                    return obj;
                }
                if (XmlParser.MatchOpenTagEnd(ref deState))
                {
                    // We need to parse sub-content
                    DeserializeSubnodes(obj);
                    // Expect close tag
                    if (!XmlParser.MatchCloseTag(ref deState, tag))
                    {
                        ThrowSyntaxError($"Missing close tag for '{tag}'!");
                    }
                    return obj;
                }
                ThrowSyntaxError($"Unexpected character: '{deState.Peek()}'!");
                return null;
            }
        }

        private void DeserializeAttributes(object obj)
        {
            // Get member infos with XmlAttribute attribute
            var objType = obj.GetType();
            var memInfos = GetXmlMembers(objType)
                .Select(e => (AttributeName(e), e))
                .Where(e => e.Item1 != null)
                .ToDictionary(e => e.Item1, e => e.Item2);
            // While we can parse an attribute, try to match it
            while (XmlParser.MatchAttribute(ref deState, out var key, out var value))
            {
                if (memInfos.TryGetValue(key, out var memInfo))
                {
                    // We have a member to assign to
                    if (serializers.TryGetValue(memInfo.FieldType, out var serializer))
                    {
                        // We have a serializer for it
                        var resultValue = serializer.Deserialize(value);
                        memInfo.SetValue(obj, resultValue);
                    }
                    else if (memInfo.FieldType.IsEnum)
                    {
                        // It's an enum, get a mapping for it
                        var enumMapping = GetEnumMapping(memInfo.FieldType);
                        var enumValue = enumMapping.NameToValue(value);
                        memInfo.SetValue(obj, enumValue);
                    }
                    else
                    {
                        throw new InvalidOperationException("Can't deserialize attribute value!");
                    }
                }
                else
                {
                    throw new UnexpectedXmlElementException(key, "attribute", objType);
                }
            }
        }

        private void DeserializeSubnodes(object obj)
        {
            // We create mappings from names, so we know what to do at each name
            var objType = obj.GetType();
            var xmlInfos = GetXmlMembers(objType);
            // We need one for inline lists: element name -> list member info
            var inlineList = new Dictionary<string, ValueMemberInfo>();
            // We need one for nested lists: outer node name -> (list member info, element name)
            var nestedList = new Dictionary<string, (ValueMemberInfo, string)>();
            // We need one for simple elements: element name -> member info
            var subnodes = new Dictionary<string, ValueMemberInfo>();
            // Fill these maps
            foreach (var memInfo in xmlInfos)
            {
                var attribName = AttributeName(memInfo);
                if (attribName != null)
                {
                    // We don't deal with attributes
                    continue;
                }
                if (ListName(memInfo, out var listName, out var elementName))
                {
                    if (listName == null)
                    {
                        // Inline
                        inlineList.Add(elementName, memInfo);
                    }
                    else
                    {
                        // Nested
                        nestedList.Add(listName, (memInfo, elementName));
                    }
                }
                else
                {
                    // Regular subnode
                    subnodes.Add(ElementName(memInfo), memInfo);
                }
            }
            // Parse
            while (XmlParser.MatchOpenTagBegin(ref deState, out var tag))
            {
                ValueMemberInfo memInfo = null;
                if (inlineList.TryGetValue(tag, out memInfo))
                {
                    // Ensure list is created
                    var list = EnsureCreated(obj, memInfo) as IList;
                    // Get the type to create
                    var elementType = memInfo.FieldType.GenericTypeArguments[0];
                    // Parse subnode
                    var subObj = DeserializeNode(memInfo, tag, elementType);
                    // Append
                    list.Add(subObj);
                }
                else if (nestedList.TryGetValue(tag, out var infos))
                {
                    memInfo = infos.Item1;
                    var elementName = infos.Item2;
                    // Ensure list is created
                    var list = EnsureCreated(obj, memInfo) as IList;
                    if (XmlParser.MatchOpenTagCompoundEnd(ref deState))
                    {
                        // Nothing to do, empty list
                    }
                    else if (XmlParser.MatchOpenTagEnd(ref deState))
                    {
                        // We parse sub-elements
                        while (!XmlParser.MatchCloseTag(ref deState, tag))
                        {
                            if (!XmlParser.MatchOpenTagBegin(ref deState, elementName))
                            {
                                ThrowSyntaxError($"Expected open tag for '{elementName}'");
                            }
                            // Get the type to create
                            var elementType = memInfo.FieldType.GenericTypeArguments[0];
                            // Parse subnode
                            var subObj = DeserializeNode(null, elementName, elementType);
                            // Append
                            list.Add(subObj);
                        }
                    }
                    else
                    {
                        ThrowSyntaxError($"Unexpected character: '{deState.Peek()}'!");
                    }
                }
                else if (subnodes.TryGetValue(tag, out memInfo))
                {
                    // Resort to subnode
                    var value = DeserializeNode(memInfo, tag, memInfo.FieldType);
                    memInfo.SetValue(obj, value);
                }
                else
                {
                    throw new UnexpectedXmlElementException(tag, "subnode", objType);
                }
            }
        }

        private EnumMapping GetEnumMapping(Type enumType)
        {
            if (enumMappings.TryGetValue(enumType, out var mapping))
            {
                return mapping;
            }
            var newMapping = new EnumMapping(enumType);
            enumMappings.Add(enumType, newMapping);
            return newMapping;
        }

        private void ThrowSyntaxError(string message) => 
            throw new XmlSyntaxException(deState.Position, message);

        private static IEnumerable<ValueMemberInfo> GetXmlMembers(Type type) =>
            type.GetValueMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(e => !e.HasAttribute<XmlIgnoreAttribute>());

        private static object EnsureCreated(object obj, ValueMemberInfo memberInfo)
        {
            var value = memberInfo.GetValue(obj);
            if (value == null)
            {
                value = DefaultConstruct(memberInfo.FieldType);
                memberInfo.SetValue(obj, value);
            }
            return value;
        }

        private static object DefaultConstruct(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                var ctor = type.GetConstructor(Type.EmptyTypes);
                return ctor.Invoke(new object[] { });
            }
        }

        private static string RootNodeName(Type rootType, out bool optional)
        {
            optional = false;
            var name = rootType.Name;
            var rootAttrib = rootType.GetCustomAttribute<XmlRootAttribute>();
            var optionalRootAttrib = rootType.GetCustomAttribute<XmlOptionalRootAttribute>();
            if (rootAttrib != null)
            {
                name = rootAttrib.ElementName;
            }
            else if (optionalRootAttrib != null)
            {
                optional = true;
                name = NonEmpty(optionalRootAttrib.ElementName, name);
            }
            if (name.Length == 0)
            {
                name = null;
            }
            return name;
        }

        private static string AttributeName(ValueMemberInfo memberInfo)
        {
            var attribAttrib = memberInfo.GetAttribute<XmlAttributeAttribute>();
            if (attribAttrib == null)
            {
                return null;
            }
            return NonEmpty(attribAttrib.AttributeName, memberInfo.Name);
        }

        private static bool ListName(ValueMemberInfo memberInfo, out string listName, out string elementName)
        {
            if (!typeof(IList).IsAssignableFrom(memberInfo.FieldType))
            {
                // Not even a list
                listName = null;
                elementName = null;
                return false;
            }
            // Read out the attributes
            var arrayAttrib = memberInfo.GetAttribute<XmlArrayAttribute>();
            var arrayItemAttrib = memberInfo.GetAttribute<XmlArrayItemAttribute>();
            if (arrayAttrib != null)
            {
                // Nested list
                Debug.Assert(arrayItemAttrib != null);
                listName = arrayAttrib.ElementName;
                elementName = arrayItemAttrib.ElementName;
                return true;
            }
            // Non-nested list
            listName = null;
            elementName = ElementName(memberInfo);
            return true;
        }

        private static string ElementName(ValueMemberInfo memberInfo)
        {
            var elemAttrib = memberInfo.GetAttribute<XmlElementAttribute>();
            return NonEmpty(elemAttrib?.ElementName, memberInfo.Name);
        }

        private static string NonEmpty(params string[] strings)
        {
            foreach (var s in strings)
            {
                if (s != null && s.Length > 0)
                {
                    return s;
                }
            }
            return null;
        }
    }
}
