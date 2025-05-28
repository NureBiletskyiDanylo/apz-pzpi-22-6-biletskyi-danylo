using MediStoS.Enums;
using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class RegisterDto
{
    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("lastname")]
    public string LastName { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("role")]
    public Roles Role { get; set; }
}
