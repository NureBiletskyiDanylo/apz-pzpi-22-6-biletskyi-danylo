namespace MediStoS.DataTransferObjects;

public class AccountDto
{
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required string Role { get; set; }
}
