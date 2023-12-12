using BlazorApp.Data;
using ForexTradingBotWebAPI.Persistence.User;
using Microsoft.AspNetCore.Mvc;

namespace ForexTradingBotWebAPI.Service.User;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;
    
    public UserService(IUserRepo context)
    {
        _userRepo = context;
    }
    
    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        return await _userRepo.GetUserAsync(email);
    }

    public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
    {
        return await _userRepo.PostUserAsync(user);
    }
    
    public async Task<bool> RemoveUserByEmail(string email)
    {
        return await _userRepo.DeleteUserAsync(email);
    }
    
    public Task<ApplicationUser> GetUserById(string id)
    {
        
        throw new NotImplementedException();
    }
    
    public Task UpdateUser(Domain.User.User user)
    {
        throw new NotImplementedException();
    }

   
}