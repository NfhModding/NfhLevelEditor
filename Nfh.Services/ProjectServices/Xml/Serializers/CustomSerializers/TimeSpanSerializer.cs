using Format.Xml.Helpers;
using System;

namespace Nfh.Services.ProjectServices.Xml.Serializers.CustomSerializers
{
    internal class TimeSpanSerializer : TypedValueSerializer<TimeSpan?>
    {
        public override TimeSpan? DeserializeTyped(string value)
        {
            if (value is null or "0")
                return null;

            var intValue = int.Parse(value);
            return TimeSpan.FromSeconds(intValue / 12);
        }

        public override string SerializeTyped(TimeSpan? value)
        {
            if (value is null)
                return "0";

            return (int.Parse(value.Value.TotalSeconds.ToString()) * 12).ToString();
        }
    }
}
