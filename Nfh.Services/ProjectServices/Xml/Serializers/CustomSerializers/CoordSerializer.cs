using Format.Xml.Helpers;
using Nfh.Services.ProjectServices.Xml.Models.Common;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class CoordSerializer : TypedValueSerializer<Coord>
    {
        public override string SerializeTyped(Coord value)
        {
            if (value == null)
            {
                return null;
            }
            return $"{value.X}/{value.Y}";
        }

        public override Coord DeserializeTyped(string value)
        {
            var parts = value.Split('/');
            return new Coord
            {
                X = int.Parse(parts[0]),
                Y = int.Parse(parts[1]),
            };
        }
    }
}
