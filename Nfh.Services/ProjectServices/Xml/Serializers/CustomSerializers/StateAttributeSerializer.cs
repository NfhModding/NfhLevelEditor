using Format.Xml.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Meta;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class StateAttributeSerializer : TypedValueSerializer<XmlLevelDataStateAttribute>
    {
        public override XmlLevelDataStateAttribute DeserializeTyped(string value) => new()
        {
            IsUnlocked = value != "locked",
        };

        public override string SerializeTyped(XmlLevelDataStateAttribute value) =>
            value.IsUnlocked ? "playable" : "locked";
    }
}
