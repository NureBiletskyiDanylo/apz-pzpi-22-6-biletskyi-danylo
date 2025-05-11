using MediStoS.Database.Models;

namespace MediStoS.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
