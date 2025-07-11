﻿using System.Text.Json.Serialization;

namespace MediStoS.DataTransferObjects;

public class LoginDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
