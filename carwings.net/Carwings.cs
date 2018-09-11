using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace carwings.net
{
    public class Carwings
    {
        // Values from http://virantha.com/2016/04/07/updated-reverse-engineering-nissan-connect-ev-protocol/
        private const string baseUrl = "https://gdcportalgw.its-mo.com/gworchest_160803EC/gdc";
        private const string initialAppString = "geORNtsZe5I4lRGjG9GZiA";
        private const string contentType = "application/x-www-form-urlencoded";

        private Region region;
        private ILoginProvider loginProvider;
        private string language = null;
        private string timezone = null;

        public Carwings(Region region, ILoginProvider loginProvider)
        {
            this.region = region;
            this.loginProvider = loginProvider;
            this.language = "en-US";
        }

        public async Task<UserLoginResponse> Login()
        {
            var encryptionKeyResponse = await ExecuteRequest<InitialAppResponse>($"{baseUrl}/InitialApp.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&initial_app_strings={initialAppString}");
            if(encryptionKeyResponse != null &&
                encryptionKeyResponse.Status == 200)
            {
                // Managed to perform initial encryption handshake. Get password from provider and encode for sending 
                string base64Password = loginProvider.GetEncryptedPassword(encryptionKeyResponse.Baseprm);
                string urlEncodedPassword = WebUtility.UrlEncode(base64Password);

                var loginResponse = await ExecuteRequest<UserLoginResponse>($"{baseUrl}/UserLoginRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&initial_app_strings={initialAppString}&UserId={loginProvider.Username}&Password={base64Password}");
                if (loginResponse != null &&
                    loginResponse.Status == 200)
                {
                    language = loginResponse.CustomerInfo.Language;
                    timezone = loginResponse.CustomerInfo.Timezone;
                }

                return loginResponse;
            }
            else
            {
                return new UserLoginResponse { Status = encryptionKeyResponse.Status, Message = encryptionKeyResponse.Message };
            }
        }

        public async Task<BatteryStatusRecordsResponse> GetBatteryStatus(VehicleInfo info, VehicleProfile vehicle)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<BatteryStatusRecordsResponse>($"{baseUrl}/BatteryStatusRecordsRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<CheckRequestResponse> RefreshBatteryStatus(VehicleInfo info, VehicleProfile vehicle)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<CheckRequestResponse>($"{baseUrl}/BatteryStatusCheckRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&UserId={vehicle.GdcUserId}&tz={this.timezone}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<BatteryStatusCheckResultResponse> CheckBatteryStatus(VehicleInfo info, VehicleProfile vehicle, CheckRequestResponse checkRequest)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<BatteryStatusCheckResultResponse>($"{baseUrl}/BatteryStatusCheckResultRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={checkRequest.Vin}&UserId={checkRequest.UserId}&tz={this.timezone}&resultKey={checkRequest.ResultKey}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<HvacStatusResponse> GetHvacStatus(VehicleInfo info, VehicleProfile vehicle)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<HvacStatusResponse>($"{baseUrl}/RemoteACRecordsRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<CheckRequestResponse> HvacOn(VehicleInfo info, VehicleProfile vehicle)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<CheckRequestResponse>($"{baseUrl}/ACRemoteRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<HvacStatusCheckResultResponse> CheckHvacOnStatus(VehicleInfo info, VehicleProfile vehicle, CheckRequestResponse checkRequest)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<HvacStatusCheckResultResponse>($"{baseUrl}/ACRemoteResult.php",$"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={checkRequest.Vin}&UserId={checkRequest.UserId}&tz={this.timezone}&resultKey={checkRequest.ResultKey}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<CheckRequestResponse> HvacOff(VehicleInfo info, VehicleProfile vehicle)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<CheckRequestResponse>($"{baseUrl}/ACRemoteOffRequest.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={vehicle.Vin}&tz={this.timezone}&custom_sessionid={urlEncodedSessionId}");
        }

        public async Task<HvacStatusCheckResultResponse> CheckHvacOffStatus(VehicleInfo info, VehicleProfile vehicle, CheckRequestResponse checkRequest)
        {
            string urlEncodedSessionId = WebUtility.UrlEncode(info.CustomSessionId);
            return await ExecuteRequest<HvacStatusCheckResultResponse>($"{baseUrl}/ACRemoteOffResult.php", $"RegionCode={this.region.ToRegionCode()}&lg={this.language}&DCMID={vehicle.DcmId}&VIN={checkRequest.Vin}&UserId={checkRequest.UserId}&tz={this.timezone}&resultKey={checkRequest.ResultKey}&custom_sessionid={urlEncodedSessionId}");
        }

        private async Task<T> ExecuteRequest<T>(string url)
        {
            return await ExecuteRequest<T>(url, String.Empty);
        }

        private async Task<T> ExecuteRequest<T>(string url, string encodedData)
        {
            var client = new HttpClient();
            var outgoingContent = new StringContent(encodedData, Encoding.ASCII, contentType);
            var response = await client.PostAsync(url, outgoingContent);

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
