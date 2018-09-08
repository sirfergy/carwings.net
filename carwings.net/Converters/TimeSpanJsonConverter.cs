using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace carwings.net
{
    public class TimeSpanJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            var hours = obj.Value<int>("HourRequiredToFull");
            var minutes = obj.Value<int>("MinutesRequiredToFull");

            return new TimeSpan(hours, minutes, 0);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
