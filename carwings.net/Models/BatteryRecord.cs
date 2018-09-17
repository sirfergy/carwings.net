using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class BatteryStatusResponse
    {
        [JsonProperty("batteryRecords")]
        public BatteryRecord BatteryRecord { get; set; }
    }

    public class BatteryRecord
    {
        [JsonProperty("lastUpdatedDateAndTime")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("batteryStatus")]
        public BatteryStatus Status { get; set; }

        [JsonProperty]
        public string PluginState { get; set; }

        /// <summary>
        /// Range in meters when the ac is on
        /// </summary>
        [JsonProperty("cruisingRangeAcOn")]
        public double CruisingRangeAcOn { get; set; }

        /// <summary>
        /// Range in meters when the ac is off
        /// </summary>
        [JsonProperty("cruisingRangeAcOff")]
        public double CruisingRangeAcOff { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(TimeRequiredJsonConverter))]
        public TimeSpan? TimeRequired { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(TimeRequiredJsonConverter))]
        public TimeSpan? TimeRequired200 { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(TimeRequiredJsonConverter))]
        public TimeSpan? TimeRequired200_6kw { get; set; }
    }

    public class BatteryStatus
    {
        [JsonProperty("batteryChargingStatus")]
        public string Charging { get; set; }

        [JsonProperty("batteryCapacity")]
        public int Capacity { get; set; }

        [JsonProperty("batteryRemainingAmount")]
        public int Remaining { get; set; }
    }
}
