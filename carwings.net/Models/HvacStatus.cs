using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace carwings.net
{
    public class HvacStatus
    {
        public string OperationResult { get; set; }

        public DateTime OperationDateAndTime { get; set; }

        public string RemoteACOperation { get; set; }

        public DateTime ACStartStopDateAndTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double CruisingRangeAcOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double CruisingRangeAcOff { get; set; }

        public string ACStartStopURL { get; set; }

        public string PluginState { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ACDurationBatterySec { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ACDurationPluggedSec { get; set; }
    }
}
