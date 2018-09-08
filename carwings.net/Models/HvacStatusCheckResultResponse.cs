using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace carwings.net
{
    public class HvacStatusCheckResultResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int HvacStatus { get; set; }

        public string Message { get; set; }

        public string OperationResult { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ResponseFlag { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
