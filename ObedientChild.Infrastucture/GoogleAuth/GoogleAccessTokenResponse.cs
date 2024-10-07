namespace ObedientChild.WebApi
{
    public class GoogleAccessTokenResponse
    {
        [Newtonsoft.Json.JsonProperty("access_token")]
        public string Token { get; set; }
        [Newtonsoft.Json.JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
