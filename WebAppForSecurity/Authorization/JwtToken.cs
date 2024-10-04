using Newtonsoft.Json;

namespace WebAppForSecurity.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken{ get; set; } = string.Empty;
        [JsonProperty("expires_at")]
        public DateTime ExpireAt { get; set; }
    }
}
