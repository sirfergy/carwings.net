using Newtonsoft.Json;

namespace carwings.net
{

    public class Vehicle
    {
        [JsonProperty("uvi")]
        public string VIN { get; set; }

        [JsonProperty]
        public string Nickname { get; set; }

        [JsonProperty("batteryRecords")]
        public BatteryRecord BatteryRecord { get; set; }
    }
}
