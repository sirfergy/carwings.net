using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class BatteryStatusResponse
    {
        [JsonProperty("batteryRecords")]
        public BatteryRecord BatteryRecord { get; set; }

        /// <summary>
        /// Probably internal temperature, but not clear
        /// </summary>
        [JsonProperty("temperatureRecords")]
        public TemperatureRecord TemperatureRecord { get; set; }
    }

    public class BatteryRecord
    {
        /// <summary>
        /// Returns time in UTC
        /// </summary>
        [JsonProperty("lastUpdatedDateAndTime")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("batteryStatus")]
        public BatteryStatus Status { get; set; }

        /// <summary>
        /// Observed values include "NOT_CONNECTED", "CONNECTED", and "QC_CONNECTED"
        /// </summary>
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

    public class StateOfChargeWrapper
    {
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class BatteryStatus
    {
        /// <summary>
        /// Valid values include "NO", "YES", and might include "NORMAL_CHARGING" and "NOT_CHARGING".  There may be others.
        /// </summary>
        [JsonProperty("batteryChargingStatus")]
        public string Charging { get; set; }

        /// <summary>
        /// Seems to be 100 often.  Not clear if this is the desired SoC, or some measurement of battery degradation (ie, 80% after 2 years in LA)
        /// </summary>
        [JsonProperty("batteryCapacity")]
        public int Capacity { get; set; }

        [JsonProperty("batteryRemainingAmount")]
        public int Remaining { get; set; }

        /// <summary>
        /// State of charge.  Note the Remaining field also seems equivalent.  It's not clear when they would differ.
        /// </summary>
        [JsonProperty("soc")]
        public StateOfChargeWrapper StateOfCharge { get; set; }
    }
}
