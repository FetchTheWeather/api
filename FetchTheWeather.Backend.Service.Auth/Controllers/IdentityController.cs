using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FetchTheWeather.Backend.Service.Auth.Models;
using FetchTheWeather.Backend.Service.Auth.Models.DTO.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FetchTheWeather.Backend.Service.Auth.Controllers;

[ApiController, Route("auth/identity")]
public class IdentityController(
    UserManager<FtwUser> userManager,
    SignInManager<FtwUser> signInManager,
    IConfiguration config
) : ControllerBase
{
    [HttpGet("me"), Authorize]
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
            UnlockedAchievements = Array.Empty<string>()
        });
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized("1 Invalid email or password");

        var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return Unauthorized("2 Invalid email or password");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, $"{user.Id}"),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Authentication:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Authentication:Issuer"],
            audience: config["Authentication:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}