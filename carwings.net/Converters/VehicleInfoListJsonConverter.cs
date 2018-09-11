using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace carwings.net
{
    public class VehicleInfoListJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<VehicleInfo>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JArray.Load(reader);
            var vehicleInfos = obj.ToObject<List<VehicleInfo>>();
            // HACK.  Json.NET merges keys ignoring case, and for some reason
            // the Nissan API returns two vehicleInfo arrays with different 
            // properties.  We only want the ones with customSessionId.
            if (vehicleInfos.Any(v => string.IsNullOrEmpty(v.CustomSessionId)))
            {
                return null;
            }

            return vehicleInfos;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
