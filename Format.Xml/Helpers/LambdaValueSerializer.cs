using System;

namespace Format.Xml.Helpers
{
    internal class LambdaValueSerializer<T> : TypedValueSerializer<T>
    {
        public Func<T, string> Serializer { get; set; }
        public Func<string, T> Deserializer { get; set; }

        public override string SerializeTyped(T value) => Serializer(value);
        public override T DeserializeTyped(string value) => Deserializer(value);
    }
}
