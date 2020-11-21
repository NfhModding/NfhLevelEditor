using System.IO;

namespace Nfh.Services.ProjectServices.Xml.Serializers
{
    internal interface ISerializer
    {
        public string Serialize(object obj);
        
        public T Deserialize<T>(string xml)
            where T : new();
        public T DeserializeFromFile<T>(FileInfo file)
            where T : new();
    }
}
