using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class ExecutionRequest
    {
        [JsonProperty]
        public DateTime ExecutionTime => DateTime.Now;
    }

    public class ExecutionResponse
    {
        [JsonProperty]
        public string MessageDeliveryStatus { get; set; }
    }
}
