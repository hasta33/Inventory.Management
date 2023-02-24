using IdentityModel.Client;

namespace InventoryManagement.API.Services
{
    public class KeycloakServiceTest
    {
        private readonly HttpClient _client;
        private readonly TokenClient _tokenClient;

        public KeycloakServiceTest(HttpClient client, TokenClient tokenClient)
        {
            _client = client;
            _tokenClient = tokenClient;
        }

        public async Task CreateResource(Resource resource)
        {
            var token = await _tokenClient.GetClientCredentialsToken();
            _client.SetBearerToken(token);
            await _client.PostAsJsonAsync("", resource);
        }
    }
}
