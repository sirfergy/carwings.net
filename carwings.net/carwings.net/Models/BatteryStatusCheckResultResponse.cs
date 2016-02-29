using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class BatteryStatusCheckResultResponse
    {
        public int Status { get; set; }

        public int ResponseFlag { get; set; }

        public string Message { get; set; }

        public string OperationResult { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int BatteryCapacity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int BatteryDegradation { get; set; }

        public string ChargeMode { get; set; }

        public string ChargeStatus { get; set; }

        public string Charging { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double CruisingRangeAcOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double CruisingRangeAcOff { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CurrentChargeLevel { get; set; }

        public string PluginState { get; set; }

        [JsonConverter(typeof(TimeSpanJsonConverter))]
        public TimeSpan TimeRequiredToFull { get; set; }

        [JsonConverter(typeof(TimeSpanJsonConverter))]
        public TimeSpan TimeRequiredToFull200 { get; set; }

        [JsonConverter(typeof(TimeSpanJsonConverter))]
        public TimeSpan TimeRequiredToFull200_6kW { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
