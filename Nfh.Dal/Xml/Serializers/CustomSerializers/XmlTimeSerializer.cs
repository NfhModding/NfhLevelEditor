using Format.Xml.Helpers;
using Nfh.Dal.Xml.Models.Objects;

namespace Nfh.Dal.Xml.Serializers.CustomSerializers
{
    internal class XmlTimeSerializer : TypedValueSerializer<XmlObjectsTime>
    {
        public override string SerializeTyped(XmlObjectsTime value) => 
            value.Amount == XmlObjectsTime.Auto.Amount ? "auto" : value.Amount.ToString();

        public override XmlObjectsTime DeserializeTyped(string value) => 
            value == "auto" ? XmlObjectsTime.Auto : new XmlObjectsTime(int.Parse(value));
    }
}
