using System.Collections.Generic;
using Newtonsoft.Json;

namespace carwings.net
{
    public class UserLoginResponse
    {
        public int Status { get; set; }

        public string Message { get; set; }
        
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        
        public string SessionId { get; set; }

        public string EncAuthToken { get; set; }

        public int UserInfoRevisionNo { get; set; }

        public CustomerInfo CustomerInfo { get; set; }

        [JsonProperty("VehicleInfoList")]
        public List<VehicleInfo> Vehicles { get; set; }

        [JsonProperty("vehicle")]
        public VehicleProfile Profile { get; set; }
    }
}
