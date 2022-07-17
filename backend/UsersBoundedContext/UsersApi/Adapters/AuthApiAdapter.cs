using UsersApi.Models;
namespace UsersApi.Adapters;

public class AuthApiAdapter
{
    private readonly HttpClient _httpClient;

    public AuthApiAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User?> CreateUserAsync(NewUserCreateRequest request)
    {
        var userRepresentation = new UserRepresentation
        {
            enabled = true,
            username = request.Email,
            email = request.Email,
            firstName = request.FirstName,
            lastName = request.LastName,
        };

        userRepresentation.credentials.Add(new CredentialType { type = "password", value = request.Password, temporary = false  });
        userRepresentation.groups.Add("web-user");
        var token = await GetAdminCliToken();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.PostAsJsonAsync("/admin/realms/Hypertheory/users", userRepresentation);
        response.EnsureSuccessStatusCode();

        var userResponse = await _httpClient.GetAsync($"http://localhost:8080/admin/realms/Hypertheory/users?username={userRepresentation.username}");
        userResponse.EnsureSuccessStatusCode();
        var userInfo = await userResponse.Content.ReadFromJsonAsync<List<User>>();
        return userInfo?.FirstOrDefault();
    }
    private async Task<string?> GetAdminCliToken()
    {
        var url = "/realms/master/protocol/openid-connect/token";
        var request = new HttpRequestMessage(HttpMethod.Post, url);
       
        var contentBody = new Dictionary<string, string>();
        contentBody.Add("grant_type", "client_credentials");
        contentBody.Add("client_id", "admin-cli");
        contentBody.Add("client_secret", "iUz1yMWr9YlzRZeIPDfFnZxO7oyIFTVG");
        var content = new FormUrlEncodedContent(contentBody);
        request.Content = content;
        
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var tokenContent = await response.Content.ReadFromJsonAsync<TokenResponse>();

        return tokenContent?.access_token;

    }
}

public class TokenResponse
{
    public string? access_token { get; set; }
    public string? token_type { get; set; }
    public int? expires_in { get; set; }
    public string? refresh_token { get; set; }
    public string? scope { get; set; }
}
public class UserRepresentation
{
    public string? email { get; set; }
    public string? username { get; set; }
  
    public bool enabled { get; set; } = true;
    public string? firstName { get; set; }
    public string? lastName { get; set; }

    public List<CredentialType> credentials { get; set; } = new List<CredentialType>();
    public List<string> groups { get; set; } = new List<string>();

}

public class CredentialType
{
    public string type { get; set; } = "password";
    public string? value { get; set; }
    public bool temporary { get; set; } = false;
}