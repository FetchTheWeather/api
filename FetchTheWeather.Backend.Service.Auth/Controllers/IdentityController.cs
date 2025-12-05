using FetchTheWeather.Backend.Service.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FetchTheWeather.Backend.Service.Auth.Controllers;

[ApiController, Route("identity")]
public class IdentityController(SignInManager<FtwUser> signInManager) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var user = await signInManager.UserManager.GetUserAsync(User);
        if (user is null) return NotFound("User not found");

        var authTypeClaim = User.Claims.FirstOrDefault(c => c.Type == "auth_type");

        var email = user.Email ?? "No email found";
        var username = user.UserName ?? "No username found";

        return Ok(new
        {
            Id = user.Id,
            AuthType = authTypeClaim?.Value ?? "unknown",

            Email = email,
            Username = username,
        });
    }
}