using Newtonsoft.Json;

namespace carwings.net
{
    public class CustomerInfo
    {
        public string UserId { get; set; }

        public string Language { get; set; }

        public string Timezone { get; set; }

        [JsonProperty("RegionCode")]
        [JsonConverter(typeof(RegionJsonConverter))]
        public Region Region { get; set; }

        public string OwnerId { get; set; }

        public string Nickname { get; set; }

        public string Country { get; set; }

        public string VehicleImage { get; set; }

        public string UserVehicleBoundDurationSec { get; set; }
    }
}
