using Newtonsoft.Json;
using System;

namespace carwings.net
{

    public class Vehicle
    {
        [JsonProperty("uvi")]
        public string VIN { get; set; }

        [JsonProperty("modelname")]
        public string ModelName { get; set; }

        [JsonProperty("modelyear")]
        public int ModelYear { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("batteryRecords")]
        public BatteryRecord BatteryRecord { get; set; }

        [JsonProperty("interiorTempRecords")]
        public TemperatureRecord InteriorTemperatureRecord { get; set; }
    }
}
