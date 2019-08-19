using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace carwings.net
{
    // Example:  "timeRequired200":{"hourRequiredToFull":5,"minutesRequiredToFull":0},
    public class TimeRequiredJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var obj = JObject.Load(reader);

            var hours = obj.Value<int>("hourRequiredToFull");
            var minutes = obj.Value<int>("minutesRequiredToFull");

            return new TimeSpan(hours, minutes, 0);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteToken(JsonToken.Null, null);
                return;
            }

            if (value == null)
                throw new ArgumentException("TimeRequiredJsonConverter expected a TimeSpan.");

            TimeSpan duration = (TimeSpan) value;

            //JToken t = JToken.FromObject(value);
            JObject o = new JObject();
            o.Add("hourRequiredToFull", duration.Hours);
            o.Add("minutesRequiredToFull", duration.Minutes);
            o.WriteTo(writer);
        }
    }
}
