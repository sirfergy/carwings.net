using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class RegionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var regionCode = (string)reader.Value;
            switch (regionCode)
            {
                case "NMA":
                    return Region.Australia ;
                case "NCI":
                    return Region.Canada;
                case "NE":
                    return Region.Europe;
                case "NML":
                    return Region.Japan;
                case "NNA":
                    return Region.USA;
            }

            throw new ArgumentOutOfRangeException(nameof(regionCode));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var region = (Region)value;
            switch (region)
            {
                case Region.Australia:
                    writer.WriteValue("NMA");
                    break;
                case Region.Canada:
                    writer.WriteValue("NCI");
                    break;
                case Region.Europe:
                    writer.WriteValue("NE");
                    break;
                case Region.Japan:
                    writer.WriteValue("NML");
                    break;
                case Region.USA:
                    writer.WriteValue("NNA");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), "Unsupported region");
            }
        }
    }
}
