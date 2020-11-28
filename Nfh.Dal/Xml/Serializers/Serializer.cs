using System;
using System.IO;
using Format.Xml;
using Nfh.Dal.Xml.Models.Common;
using Nfh.Dal.Xml.Models.LevelData;
using Nfh.Dal.Xml.Models.Objects;
using Nfh.Dal.Xml.Serializers.CustomSerializers;

namespace Nfh.Dal.Xml.Serializers
{
    internal class Serializer : ISerializer
    {
        private XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

        public Serializer()
        {
            // serializer.RegisterValue(typeof(Position), new PositionSerializer());
            serializer.RegisterValue(typeof(XmlLevelDataStateAttribute), new StateAttributeSerializer());
            serializer.RegisterValue(typeof(TimeSpan?), new TimeSpanSerializer());
            serializer.RegisterValue(typeof(TimeSpan), new TimeSpanSerializer());
            serializer.RegisterValue(typeof(XmlCoord), new CoordSerializer());
            serializer.RegisterValue(typeof(XmlObjectsTime), new XmlTimeSerializer());
            serializer.RegisterValue(typeof(int?), new NullableIntegerSerializer());
            serializer.RegisterValue(typeof(bool?), new NullableBoolSerializer());
        }

        public string Serialize(object obj) =>
            serializer.Serialize(obj);

        public T Deserialize<T>(string xml)
                where T : new() =>
            serializer.Deserialize<T>(xml);

        public T DeserializeFromFile<T>(FileInfo file) where T : new()
        {
            if (!file.Exists)
                throw new("DeserializeFromFile");

            var source = File.ReadAllText(file.FullName);
            return Deserialize<T>(source);
        }

        public void SerializeToFile(object obj, FileInfo file)
        {
            var serialized = Serialize(obj);
            File.WriteAllText(file.FullName, serialized);
        }
    }
}
