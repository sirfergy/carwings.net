using Newtonsoft.Json;

namespace carwings.net
{
    public class AuthenticateRequest
    {
        public AuthenticateRequest(Authenticate authenticate)
        {
            this.Authenticate = authenticate;
        }

        [JsonProperty]
        public Authenticate Authenticate { get; set; }
    }

    public class AuthenticateResponse
    {
        [JsonProperty]
        public string AccountId { get; set; }

        [JsonProperty]
        public string AuthToken { get; set; }

        [JsonProperty]
        public Vehicle[] Vehicles { get; set; }
    }

    public class Authenticate
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }
        [JsonProperty("brand-s")]
        public string Brand { get; set; }
        [JsonProperty("language-s")]
        public string Language { get; set; }
        [JsonProperty]
        public string Password { get; set; }
        [JsonProperty]
        public string Country { get; set; }
    }
}
