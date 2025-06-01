using MediStoS_MobileClient.DataTransferModels;
using MediStoS_MobileClient.ServerValues;
using System.Text;
using System.Text.Json;

namespace MediStoS_MobileClient.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    public AuthService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(ServerData.API_URL)
        };
    }
    public async Task<AccountDto?> Login(string email, string password)
    {
        var loginDto = new LoginDto
        {
            Email = email,
            Password = password
        };

        var json = JsonSerializer.Serialize(loginDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync("/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AccountDto>(responseContent);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
