using Newtonsoft.Json;

namespace carwings.net
{
    public class VehicleInfo
    {
        public bool Charger20066 { get; set; }

        [JsonProperty("custom_sessionid")]
        public string CustomSessionId { get; set; }

        public string Nickname { get; set; }

        public bool TelematicsEnabled { get; set; }

        public string Vin { get; set; }
    }
}
