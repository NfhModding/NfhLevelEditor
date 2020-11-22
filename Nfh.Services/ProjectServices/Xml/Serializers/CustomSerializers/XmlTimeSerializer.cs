using Format.Xml.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Objects;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class XmlTimeSerializer : TypedValueSerializer<XmlTime>
    {
        public override string SerializeTyped(XmlTime value) => 
            value.Amount == XmlTime.Auto.Amount ? "auto" : value.Amount.ToString();

        public override XmlTime DeserializeTyped(string value) => 
            value == "auto" ? XmlTime.Auto : new XmlTime(int.Parse(value));
    }
}
