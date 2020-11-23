using Format.Xml.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Objects;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class XmlTimeSerializer : TypedValueSerializer<XmlObjectsTime>
    {
        public override string SerializeTyped(XmlObjectsTime value) => 
            value.Amount == XmlObjectsTime.Auto.Amount ? "auto" : value.Amount.ToString();

        public override XmlObjectsTime DeserializeTyped(string value) => 
            value == "auto" ? XmlObjectsTime.Auto : new XmlObjectsTime(int.Parse(value));
    }
}
