namespace FetchTheWeather.Backend.Service.Auth.Models.DTO.Login;

public class LoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}