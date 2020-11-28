using Format.Xml.Helpers;
using Nfh.Dal.Xml.Models.LevelData;

namespace Nfh.Dal.Xml.Serializers.CustomSerializers
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
