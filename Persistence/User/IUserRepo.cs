using BlazorApp.Data;
using ForexTradingBotWebAPI.Domain.User;
using Microsoft.AspNetCore.Mvc;

namespace ForexTradingBotWebAPI.Persistence.User;

public interface IUserRepo
{
    Task<ApplicationUser> PostUserAsync(ApplicationUser user);
    Task<ApplicationUser> GetUserAsync(string email);
    Task<Domain.User.User> GetUserAsync(string email, string password);
    Task<bool> DeleteUserAsync(string email);
}