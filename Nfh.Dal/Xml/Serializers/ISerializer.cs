using System.IO;

namespace Nfh.Dal.Xml.Serializers
{
    internal interface ISerializer
    {
        public string Serialize(object obj);
        public void SerializeToFile(object obj, FileInfo file);

        public T Deserialize<T>(string xml)
            where T : new();
        public T DeserializeFromFile<T>(FileInfo file)
            where T : new();
    }
}
