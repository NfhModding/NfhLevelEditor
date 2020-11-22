using Format.Xml;
using Nfh.Services.ProjectServices.Xml.Models.Common;
using Nfh.Services.ProjectServices.Xml.Models.Meta;
using Nfh.Services.ProjectServices.Xml.Models.Objects;
using Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers;
using System;
using System.IO;

namespace Nfh.Services.ProjectServices.Xml.Serializers
{
    internal class Serializer : ISerializer
    {
        private XmlSerializer serializer = XmlSerializer.WithDefaultSerializers();

        public Serializer()
        {
            // serializer.RegisterValue(typeof(Position), new PositionSerializer());
            serializer.RegisterValue(typeof(StateAttribute), new StateAttributeSerializer());
            serializer.RegisterValue(typeof(TimeSpan?), new TimeSpanSerializer());
            serializer.RegisterValue(typeof(TimeSpan), new TimeSpanSerializer());
            serializer.RegisterValue(typeof(Coord), new CoordSerializer());
            serializer.RegisterValue(typeof(XmlTime), new XmlTimeSerializer());
            serializer.RegisterValue(typeof(int?), new NullableIntegerSerializer());
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
