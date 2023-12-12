using BlazorApp.Data;
using ForexTradingBotWebAPI.Domain.User;
using ForexTradingBotWebAPI.Service.User;
using Microsoft.AspNetCore.Mvc;

namespace ForexTradingBotWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUser", Name = "GetUser")]
    public async Task<ActionResult<ApplicationUser>> GetUser(String userEmail)
    {
        Console.WriteLine("looking up "+userEmail);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var tempUser = await _userService.GetUserByEmailAsync(userEmail);

            if (tempUser == null)
            {
                return NotFound($"User with email '{userEmail}' not found");
            }

            Console.WriteLine("found user "+tempUser.Email);
            return tempUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("CreateUser")]    
    public async Task<ActionResult<User>> CreateUser([FromBody] ApplicationUser user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _userService.CreateUserAsync(user);
            return Created($"/{user.Email}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("DeleteUser")]
    public async Task<ActionResult> DeleteUser(string userEmail)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _userService.RemoveUserByEmail(userEmail);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}