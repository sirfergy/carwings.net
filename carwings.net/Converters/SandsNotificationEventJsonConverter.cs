using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace carwings.net
{
    public class SandsNotificationEventJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Location);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var obj = JObject.Load(reader);

            var sandsNotificationEvent = obj.SelectToken("sandsNotificationEvent.body.location");

            return sandsNotificationEvent.ToObject<Location>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
