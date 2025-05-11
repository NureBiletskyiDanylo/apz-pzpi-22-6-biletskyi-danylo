namespace MediStoS.Controllers;

using MediStoS.Database.Models;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.DataTransferObjects;
using MediStoS.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    [Route("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetUser(int id)
    {
        UserDto? user = await userRepository.GetUserAsync(id);
        if (user == null) return NotFound($"User with specified id : {id} was not found");

        return Ok(user);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "Admin,DBAdmin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        bool result = await userRepository.DeleteUserByIdAsync(id);
        if (!result) return BadRequest("User was not deleted");
        return Ok();
    }

    [HttpPut]
    [Route("edit")]
    [Authorize(Roles = "Admin,DBAdmin")]
    public async Task<IActionResult> UpdateUser(UserDto model)
    {
        if (model == null)
        {
            return BadRequest("Update user model was corrupted");
        }

        bool result = await userRepository.UpdateUserAsync(model);
        if (!result) return BadRequest("User was not updated");
        return Ok(model);
    }

    [HttpGet]
    [Route("users")]
    [Authorize(Roles = "Admin,DBAdmin")]
    public async Task<IActionResult> GetUsers()
    {
        List<UserDto> users = await userRepository.GetUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    [Route("update-role/{id:int}")]
    [Authorize(Roles = "Admin,DBAdmin")]
    public async Task<ActionResult> UpdateUserRole(int id, [FromBody] string role)
    {
        Roles roleValue = Enum.Parse<Roles>(role);

        bool success = await userRepository.UpdateUserRoleAsync(id, roleValue);
        if (success) return Ok();
        return BadRequest("User role was not updated due to internal error");
    }
}
