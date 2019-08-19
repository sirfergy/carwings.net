using Newtonsoft.Json;
using System;


namespace carwings.net
{
    public class TemperatureRecord
    {
        [JsonProperty("operationResult")]
        public String OperationResult { get; set; }

        [JsonProperty("operationDateAndTime")]
        public DateTimeOffset? OperationDateAndTime { get; set; }

        [JsonProperty("notificationDateAndTime")]
        public DateTimeOffset? NotificationDateAndTime { get; set; }

        // Returns a value in Celsius
        [JsonProperty("inc_temp")]
        public float IncTemp { get; set; }
    }
}
