using Format.Xml.Helpers;

namespace Nfh.Dal.Xml.Serializers.CustomSerializers
{
    internal class NullableBoolSerializer : TypedValueSerializer<bool?>
    {
        public override bool? DeserializeTyped(string value)
        {
            if (bool.TryParse(value, out var result))
                return result;
            return null;
        }

        public override string SerializeTyped(bool? value) =>
            value?.ToString() ?? string.Empty;
    }
}
