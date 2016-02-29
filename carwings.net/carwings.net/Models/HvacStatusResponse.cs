using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class HvacStatusResponse
    {
        public int Status { get; set; }

        public string Message { get; set; }

        [JsonProperty("RemoteACRecords")]
        public HvacStatus HvacStatus { get; set; }

        public DateTime OperationDateAndTime { get; set; }
    }
}
