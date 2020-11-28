using Format.Xml.Helpers;

namespace Nfh.Dal.Xml.Serializers.CustomSerializers
{
    internal class NullableIntegerSerializer : TypedValueSerializer<int?>
    {
        public override int? DeserializeTyped(string value)
        {
            if (int.TryParse(value, out var result))
                return result;
            return null;
        }

        public override string SerializeTyped(int? value) =>
            value?.ToString() ?? string.Empty;
    }
}
