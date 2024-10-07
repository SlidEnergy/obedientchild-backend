using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObedientChild.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ObedientChild.Infrastructure.GoogleAuth
{
    public class GoogleTokenService : IGoogleTokenService
    {
        private static readonly string TokenUrl = "https://oauth2.googleapis.com/token";
        private readonly GoogleAuthOptions _options;

        public GoogleTokenService(GoogleAuthOptions options)
        {
            _options = options;
        }

        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _options.ClientId},
                    {"client_secret", _options.ClientSecret},
                    {"refresh_token", refreshToken},
                    { "grant_type", "refresh_token" }
                });

                var response = await httpClient.PostAsync(TokenUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseContent);
                    var newAccessToken = json["access_token"]?.ToString();
                    return newAccessToken;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error refreshing access token: {response.StatusCode}, {errorContent}");
                }
            }
        }

        public async Task<GoogleAccessTokenResponse> GetAccessTokenAsync(string authorizationCode)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", authorizationCode },
                    {"client_id", _options.ClientId},
                    {"client_secret", _options.ClientSecret},
                    {"redirect_uri", _options.RedirectUrl},
                    { "grant_type", "authorization_code" }
                });

                var response = await client.PostAsync(TokenUrl, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<GoogleAccessTokenResponse>(responseContent);

                return tokenResponse;
            }
        }
    }
}
