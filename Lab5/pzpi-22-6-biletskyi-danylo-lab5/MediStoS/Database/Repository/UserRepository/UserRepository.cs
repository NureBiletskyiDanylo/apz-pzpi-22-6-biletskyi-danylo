using AutoMapper;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Models;
using MediStoS.DataTransferObjects;
using MediStoS.Enums;
using Microsoft.EntityFrameworkCore;

namespace MediStoS.Database.Repository.UserRepository;

public class UserRepository(ApplicationDbContext context, IMapper mapper) : IUserRepository
{
    public async Task<UserDto?> GetUserAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        if (user == null) throw new ArgumentNullException("User was not found");

        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<UserDto?> GetUserAsync(string email)
    {
        User? user = await context.Users.FirstOrDefaultAsync(a => a.Email == email);
        if (user == null) throw new ArgumentNullException("User was not found");

        var userDto = mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<bool> AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) throw new ArgumentNullException("User was not found");

        context.Users.Remove(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserAsync(UserDto user)
    {
        var originalUser = await context.Users.FindAsync(user.Id);
        if (originalUser == null) throw new ArgumentNullException("User was not found");

        originalUser.FirstName = user.FirstName;
        originalUser.LastName = user.LastName;
        originalUser.Email = user.Email;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        List<User> users = await context.Users.ToListAsync();
        if (users == null) return new List<UserDto>();
        var userDtos = mapper.Map<List<UserDto>>(users);
        return userDtos;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == email);
        return user;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await context.Users.AnyAsync(a => a.Email == email);
    }

    public async Task<bool> UpdateUserRoleAsync(int userId, Roles role)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null) throw new ArgumentNullException("User was not found");

        user.Role = role;
        return await context.SaveChangesAsync() > 0;
    }
}
