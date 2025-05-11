using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using MediStoS.Enums;

namespace MediStoS.Database.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserByIdAsync(int id);
        Task<UserDto?> GetUserAsync(int id);
        Task<UserDto?> GetUserAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
        Task<List<UserDto>> GetUsersAsync();
        Task<bool> UpdateUserAsync(UserDto user);
        Task<bool> UpdateUserRoleAsync(int userId, Roles role);
    }
}