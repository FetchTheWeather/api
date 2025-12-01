

using FetchTheWeather.Backend.Service.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FetchTheWeather.Backend.Service.Auth.Data;

public class AuthDataContext(DbContextOptions options) : IdentityDbContext<FtwUser, FtwRole, Guid>(options)
{
}