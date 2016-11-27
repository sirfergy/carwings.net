using Newtonsoft.Json;
using System;

namespace carwings.net
{
    public class HvacStatusCheckResultResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string HvacStatus { get; set; }

        public string Message { get; set; }

        public string OperationResult { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ResponseFlag { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
