using BlazorApp.Data;
using ForexTradingBotWebAPI.Database;
using ForexTradingBotWebAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForexTradingBotWebAPI.Persistence.User;

public class UserRepo : IUserRepo
{
    private readonly PlatformDBContext _context;
    private readonly ApplicationUserDbContext _applicationUserDbContext;
    
    
    public UserRepo(PlatformDBContext context, ApplicationUserDbContext applicationUserDbContext)
    {
        _context = context;
        _applicationUserDbContext = applicationUserDbContext;
    }
    
    public async Task<ApplicationUser> PostUserAsync(ApplicationUser user)
    {
        var first = await _applicationUserDbContext.AspNetUsers.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));

        if (first != null) throw new Exception(Status.UserAlreadyExists);

        await _applicationUserDbContext.AspNetUsers.AddAsync(user);
        await _applicationUserDbContext.SaveChangesAsync();

        return user;
    }
    
    public async Task<ApplicationUser> GetUserAsync(string email)
    {
        return await _applicationUserDbContext.AspNetUsers.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Domain.User.User> GetUserAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(string email)
    {
        try
        {
            if (email != null)
            {
                var deletedUser = await _context.Users.FirstOrDefaultAsync(a => a.Email.Equals(email));

                if (deletedUser != null)
                {
                    _context.Users.Remove(deletedUser);
                    await _context.SaveChangesAsync();
                    return true; // Indicate success
                }
                else
                {
                    return false; // Indicate user not found
                }
            }
            else
            {
                // You might also consider throwing an ArgumentNullException here
                throw new ArgumentException("Invalid email parameter.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            // You might also consider throwing a specific exception here
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

}