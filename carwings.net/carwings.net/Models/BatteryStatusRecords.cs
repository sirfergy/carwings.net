using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class BatteryStatusRecords
    {
        public string OperationResult { get; set; }

        public string OperationDateAndTime { get; set; }

        public BatteryStatus BatteryStatus { get; set; }

        public string PluginState { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double CruisingRangeAcOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double CruisingRangeAcOff { get; set; }

        [JsonConverter(typeof(TimeSpanJsonConverter))]
        public TimeSpan TimeRequiredToFull200_6kW { get; set; }

        public DateTime NotificationDateAndTime { get; set; }

        public DateTime TargetDate { get; set; }
    }
}
