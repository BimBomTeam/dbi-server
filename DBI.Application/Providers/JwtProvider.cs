using DBI.Infrastructure.Providers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace DBI.Application.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly HttpClient httpClient;

        public JwtProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetForCredentialsAsync(string email, string password)
        {
            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var response = await httpClient.PostAsJsonAsync("", request);

            var authToken = await response.Content.ReadFromJsonAsync<AuthToken>();

            return authToken.IdToken;
        }

        public class AuthToken
        {
            [JsonPropertyName("uid")]
            public string UserId { get; set; }
            [JsonPropertyName("email")]
            public string Email { get; set; }
            [JsonPropertyName("displayName")]
            public string DisplayName { get; set; }

            [JsonPropertyName("idToken")]
            public string IdToken { get; set; }
        }
    }
}
