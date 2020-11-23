using Format.Xml.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Common;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class CoordSerializer : TypedValueSerializer<XmlCoord>
    {
        public override string SerializeTyped(XmlCoord value)
        {
            if (value == null)
            {
                return null;
            }
            return $"{value.X}/{value.Y}";
        }

        public override XmlCoord DeserializeTyped(string value)
        {
            var parts = value.Split('/');
            return new XmlCoord
            {
                X = int.Parse(parts[0]),
                Y = int.Parse(parts[1]),
            };
        }
    }
}
