using Format.Xml.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Meta;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class StateAttributeSerializer : TypedValueSerializer<StateAttribute>
    {
        public override StateAttribute DeserializeTyped(string value) => new()
        {
            IsUnlocked = value != "locked",
        };

        public override string SerializeTyped(StateAttribute value) =>
            value.IsUnlocked ? "playable" : "locked";
    }
}
