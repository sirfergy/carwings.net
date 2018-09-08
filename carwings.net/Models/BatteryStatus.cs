using Newtonsoft.Json;

namespace carwings.net
{
    public class BatteryStatus
    {
        public string BatteryChargingStatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int BatteryCapacity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int BatteryRemainingAmount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int BatteryRemainingAmountWH { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int BatteryRemainingAmountkWH { get; set; }
    }
}
