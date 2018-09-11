using Newtonsoft.Json;
using System.Collections.Generic;

namespace carwings.net
{
    public class UserLoginResponse
    {
        public class VehicleProfileWrapper
        {
            public VehicleProfile Profile { get; set; }
        }

        public class VehicleInfoWrapper
        {
            [JsonProperty("VehicleInfo")]
            [JsonConverter(typeof(VehicleInfoListJsonConverter))]
            public List<VehicleInfo> VehicleInfos{ get; set; }
        }

        public int Status { get; set; }

        public string Message { get; set; }
        
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        
        public string EncAuthToken { get; set; }

        public int UserInfoRevisionNo { get; set; }

        public CustomerInfo CustomerInfo { get; set; }

        [JsonProperty("VehicleInfoList")]
        public VehicleInfoWrapper InfoWrapper { get; set; }

        [JsonProperty("vehicle")]
        public VehicleProfileWrapper ProfileWrapper { get; set; }

        public VehicleProfile Profile { get { return ProfileWrapper.Profile; } }

        public List<VehicleInfo> Vehicles { get { return InfoWrapper.VehicleInfos; } }

    }
}
