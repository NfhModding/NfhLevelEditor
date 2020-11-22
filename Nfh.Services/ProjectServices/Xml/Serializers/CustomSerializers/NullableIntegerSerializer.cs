using Format.Xml.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
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
