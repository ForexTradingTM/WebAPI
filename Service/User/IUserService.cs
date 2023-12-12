using BlazorApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ForexTradingBotWebAPI.Service.User;

public interface IUserService
{
    Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
    Task<ApplicationUser> GetUserById(string id);
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task UpdateUser(Domain.User.User user);
    Task<bool> RemoveUserByEmail(string email);
}