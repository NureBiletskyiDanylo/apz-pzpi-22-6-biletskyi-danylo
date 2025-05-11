namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.DataTransferObjects;
using MediStoS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserRepository userRepository, ITokenService tokenService) : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AccountDto>> Login([FromBody] LoginDto loginDto)
    {
        if (loginDto == null)
        {
            return BadRequest("Login data was not received. Can't log in");
        }

        User? user = await userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null) return BadRequest("An account with specified email was not found");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid credentials");
        }

        return Ok(new AccountDto
        {
            Email = user.Email,
            Token = tokenService.CreateToken(user),
            Role = user.Role.ToString()
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<AccountDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            return BadRequest("Register data was not received. Can't sing up");
        }

        if (await userRepository.UserExistsAsync(registerDto.Email)) return BadRequest("An account with specified email already exists");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        bool result = await userRepository.AddUserAsync(user);
        if (!result) return BadRequest("User account has not been created successfully");

        return Ok(new AccountDto
        {
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = tokenService.CreateToken(user)
        });        
    }
}
