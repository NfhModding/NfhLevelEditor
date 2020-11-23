using Format.Xml.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
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
