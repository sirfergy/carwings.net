using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace carwings.net
{
    public class Carwings
    {
        private const string baseUrl = "https://gdcportalgw.its-mo.com/orchestration_1111/gdc";

        private Region region;
        private string userId;
        private string password;
        private string language = null;
        private string timezone = null;

        public Carwings(Region region, string userId, string password)
        {
            this.region = region;
            this.userId = userId;
            this.password = password;
        }

        public async Task<UserLoginResponse> Login()
        {
            var loginResponse = await ExecuteRequest<UserLoginResponse>($"{baseUrl}/UserLoginRequest.php?RegionCode={this.region.ToRegionCode()}&UserId={this.userId}&Password={this.password}");
            if (loginResponse != null &&
                loginResponse.Status == 200)
            {
                language = loginResponse.CustomerInfo.Language;
                timezone = loginResponse.CustomerInfo.Timezone;
            }

            return loginResponse;
        }

        public async Task<BatteryStatusRecordsResponse> GetBatteryStatus(VehicleProfile vehicle)
        {
            return await ExecuteRequest<BatteryStatusRecordsResponse>($"{baseUrl}/BatteryStatusRecordsRequest.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}");
        }

        public async Task<CheckRequestResponse> RefreshBatteryStatus(VehicleProfile vehicle)
        {
            return await ExecuteRequest<CheckRequestResponse>($"{baseUrl}/BatteryStatusCheckRequest.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&UserId={vehicle.GdcUserId}&tz={this.timezone}");
        }

        public async Task<BatteryStatusCheckResultResponse> CheckBatteryStatus(VehicleProfile vehicle, CheckRequestResponse checkRequest)
        {
            return await ExecuteRequest<BatteryStatusCheckResultResponse>($"{baseUrl}/BatteryStatusCheckResultRequest.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={checkRequest.Vin}&UserId={checkRequest.UserId}&tz={this.timezone}&resultKey={checkRequest.ResultKey}");
        }

        public async Task<HvacStatusResponse> GetHvacStatus(VehicleProfile vehicle)
        {
            return await ExecuteRequest<HvacStatusResponse>($"{baseUrl}/RemoteACRecordsRequest.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}");
        }

        public async Task<CheckRequestResponse> HvacOn(VehicleProfile vehicle)
        {
            return await ExecuteRequest<CheckRequestResponse>($"{baseUrl}/ACRemoteRequest.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}");
        }

        public async Task<HvacStatusCheckResultResponse> CheckHvacOnStatus(VehicleProfile vehicle, CheckRequestResponse checkRequest)
        {
            return await ExecuteRequest<HvacStatusCheckResultResponse>($"{baseUrl}/ACRemoteResult.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={checkRequest.Vin}&UserId={checkRequest.UserId}&tz={this.timezone}&resultKey={checkRequest.ResultKey}");
        }

        public async Task<CheckRequestResponse> HvacOff(VehicleProfile vehicle)
        {
            return await ExecuteRequest<CheckRequestResponse>($"{baseUrl}/ACRemoteOffRequest.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}");
        }

        public async Task<HvacStatusCheckResultResponse> CheckHvacOffStatus(VehicleProfile vehicle, CheckRequestResponse checkRequest)
        {
            return await ExecuteRequest<HvacStatusCheckResultResponse>($"{baseUrl}/ACRemoteOffResult.php?RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={checkRequest.Vin}&UserId={checkRequest.UserId}&tz={this.timezone}&resultKey={checkRequest.ResultKey}");
        }

        private async Task<T> ExecuteRequest<T>(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                Exception e = new Exception("Unexpected Response from Carwings: " + response.StatusCode);
                e.Data["URL"] = url;
                e.Data["ResponseStatusCode"] = response.StatusCode;
                if (response.Content != null)
                {
                    e.Data["Response"] = response.Content.ReadAsStringAsync().Result;
                }
                throw e;
            }
        }
    }
}
