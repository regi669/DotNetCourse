using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetCourseNew.Configuration;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Exceptions;
using DotNetCourseNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCourseNew.Services.Implementation;

public class AccountService : IAccountService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthConfig _authConfig;

    public AccountService(RestaurantDbContext dbContext, 
        IPasswordHasher<User> passwordHasher, 
        AuthConfig authConfig)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authConfig = authConfig;
    }
    public void Register(RegisterUserDTO dto)
    {
        var netUser = new User()
        {
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            Nationality = dto.Nationality,
            RoleId = dto.RoleId
        };
        var passwordHash = _passwordHasher.HashPassword(netUser, dto.Password);
        netUser.PasswordHash = passwordHash;
        _dbContext.Users.Add(netUser);
        _dbContext.SaveChanges();
    }

    public string Login(LoginUserDto dto)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == dto.Email);
        if (user is null) throw new BadRequestException("Email or Password is invalid");
        var isPasswordValid = _passwordHasher
                                  .VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Success;
        if (!isPasswordValid) throw new BadRequestException("Email or Password is invalid");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy/mm/dd")),
            new Claim("Nationality", user.Nationality)
            
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.JWTKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authConfig.JWTExpireDays);

        var token = new JwtSecurityToken(_authConfig.JWTIssuer,
            _authConfig.JWTIssuer,
            claims,
            expires: expires,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}