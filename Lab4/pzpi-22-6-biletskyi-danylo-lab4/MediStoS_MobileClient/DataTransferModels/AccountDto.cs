using System.Text.Json.Serialization;

namespace MediStoS_MobileClient.DataTransferModels;

public class AccountDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("token")]
    public required string Token { get; set; }
    [JsonPropertyName("role")]
    public required string Role { get; set; }
}
