using System;
using Newtonsoft.Json;

namespace carwings.net
{
    public class VehicleLocatorRequest
    {
        private static DateTime lastMonth = DateTime.Now.AddMonths(-1);

        [JsonProperty]
        public string ServiceName => "MyCarFinderResult";

        [JsonProperty]
        public int AcquiredDataUpperLimit => 1;

        [JsonProperty]
        public string SearchPeriod => $"{lastMonth.ToString("yyyyMMdd")},{DateTime.Now.ToString("yyyyMMdd")}";
    }

    public class VehicleLocatorResponse
    {
        [JsonProperty("sandsNotificationEvent")]
        [JsonConverter(typeof(SandsNotificationEventJsonConverter))]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("latitudeDMS")]
        public double Latitude { get; set; }

        [JsonProperty("longitudeDMS")]
        public double Longitude { get; set; }

        /// <summary>
        /// Position can be AVAILABLE, and potentially other values.
        /// </summary>
        public String Position { get; set; }
        // There are many more fields in the Location that we are not interested in, such as the lat/long in minutes & seconds.
    }
}
