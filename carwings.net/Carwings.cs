using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace carwings.net
{
    public class Carwings
    {
        private const string baseUrl = "https://icm.infinitiusa.com/NissanLeafProd/rest";
        private const string apiKey = "f950a00e-73a5-11e7-8cf7-a6006ad3dba0";
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly HttpClientHandler handler;
        private CookieContainer cookies;
        private string accountId = null;
        private string authToken = null;

        public Carwings()
        {
            this.cookies = new CookieContainer();
            this.handler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
            };
        }

        /// <summary>
        /// Log in to the specified Nissan account
        /// </summary>
        /// <param name="userid">Email address</param>
        /// <param name="password">password</param>
        /// <param name="country">Country or Nissan region name, different from Nissan's V1 API.  Valid values are USA: "US"</param>
        /// <param name="cancellationToken">Support for cancellation.</param>
        /// <returns>An array of Vehicles for this user.  If the login credentials were wrong, it returns a null Vehicle[].</returns>
        public async Task<Vehicle[]> Login(string userid, string password, string country)
        {
            return await Login(userid, password, country, CancellationToken.None);
        }

        /// <summary>
        /// Log in to the specified Nissan account
        /// </summary>
        /// <param name="userid">Email address</param>
        /// <param name="password">password</param>
        /// <param name="country">Country or Nissan region name, different from Nissan's V1 API.  Valid values are USA: "US"</param>
        /// <param name="cancellationToken">Support for cancellation.</param>
        /// <returns>An array of Vehicles for this user.  If the login credentials were wrong, it returns a null Vehicle[].</returns>
        public async Task<Vehicle[]> Login(string userid, string password, string country, CancellationToken cancellationToken)
        {
            var data = new AuthenticateRequest(
                new Authenticate
                {
                    Brand = "N",
                    Country = country,
                    Language = "en",
                    UserId = userid,
                    Password = password
                });

            var json = JsonConvert.SerializeObject(data, serializerSettings);
            var response = await PostRequest<AuthenticateResponse>($"{baseUrl}/auth/authenticationForAAS", json, cancellationToken);
            this.accountId = response.AccountId;
            this.authToken = response.AuthToken;

            return response.Vehicles;
        }

        public async Task<BatteryStatusResponse> RefreshBatteryStatus(string vin)
        {
            return await GetRequest<BatteryStatusResponse>($"{baseUrl}/battery/vehicles/{vin}/getChargingStatusRequest", 
                CancellationToken.None);
        }

        public async Task<BatteryStatusResponse> RefreshBatteryStatus(string vin, CancellationToken cancellationToken)
        {
            return await GetRequest<BatteryStatusResponse>($"{baseUrl}/battery/vehicles/{vin}/getChargingStatusRequest", cancellationToken);
        }

        public async Task<ExecutionResponse> HvacOn(string vin)
        {
            return await HvacOn(vin, CancellationToken.None);
        }

        public async Task<ExecutionResponse> HvacOn(string vin, CancellationToken cancellationToken)
        {
            var data = new ExecutionRequest();
            var json = JsonConvert.SerializeObject(data, serializerSettings);
            return await PostRequest<ExecutionResponse>($"{baseUrl}/hvac/vehicles/{vin}/activateHVAC", json, cancellationToken);
        }

        public async Task<ExecutionResponse> HvacOff(string vin)
        {
            return await HvacOff(vin, CancellationToken.None);
        }

        public async Task<ExecutionResponse> HvacOff(string vin, CancellationToken cancellationToken)
        {
            var data = new ExecutionRequest();
            var json = JsonConvert.SerializeObject(data, serializerSettings);
            return await PostRequest<ExecutionResponse>($"{baseUrl}/hvac/vehicles/{vin}/deactivateHVAC", json, cancellationToken);
        }

        public async Task<ExecutionResponse> ChargeOn(string vin)
        {
            return await ChargeOn(vin, CancellationToken.None);
        }

        public async Task<ExecutionResponse> ChargeOn(string vin, CancellationToken cancellationToken)
        {
            var data = new ExecutionRequest();
            var json = JsonConvert.SerializeObject(data, serializerSettings);
            return await PostRequest<ExecutionResponse>($"{baseUrl}/battery/vehicles/{vin}/remoteChargingRequest'", json, cancellationToken);
        }

        public async Task<ExecutionResponse> ChargeOff(string vin)
        {
            return await ChargeOff(vin, CancellationToken.None);
        }

        public async Task<ExecutionResponse> ChargeOff(string vin, CancellationToken cancellationToken)
        {
            var data = new ExecutionRequest();
            var json = JsonConvert.SerializeObject(data, serializerSettings);
            return await PostRequest<ExecutionResponse>($"{baseUrl}/battery/vehicles/{vin}/cancelRemoteChargingRequest", json, cancellationToken);
        }

        public async Task<Location> FindVehicle(string vin)
        {
            return await FindVehicle(vin, CancellationToken.None);
        }

        public async Task<Location> FindVehicle(string vin, CancellationToken cancellationToken)
        {
            var data = new { };
            var json = JsonConvert.SerializeObject(data, serializerSettings);
            var result = await PostRequest<VehicleLocatorResponse>($"{baseUrl}/vehicleLocator/vehicles/{vin}/refreshVehicleLocator", json, cancellationToken);
            return result.Location;
        }

        private async Task<T> GetRequest<T>(string url, CancellationToken cancellationToken)
        {
            var client = new HttpClient(this.handler);
            client.DefaultRequestHeaders.Add("api-key", apiKey);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", this.authToken);

            return await HandleResponse<T>(() =>
            {
                return client.GetAsync(url, cancellationToken);
            },
            cancellationToken);
        }

        private async Task<T> PostRequest<T>(string url, string data, CancellationToken cancellationToken)
        {
            var client = new HttpClient(this.handler);
            if (!string.IsNullOrEmpty(this.authToken))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", this.authToken);
            }

            var postContent = new StringContent(data);
            postContent.Headers.Add("api-key", apiKey);
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await HandleResponse<T>(() =>
            {
                return client.PostAsync(url, postContent, cancellationToken);
            },
            cancellationToken);
        }

        private async Task<T> HandleResponse<T>(Func<Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
        {
            var response = await action();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                Exception e = new Exception("Unexpected Response from Carwings: " + response.StatusCode);
                e.Data["URL"] = response.RequestMessage.RequestUri;
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
