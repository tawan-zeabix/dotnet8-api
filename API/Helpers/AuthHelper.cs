using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers;

public class AuthHelper
{
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<string> _hasher;

    public AuthHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _hasher = new PasswordHasher<string>();
    }

    public string GenerateToken(UserModel user)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? ""));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        JwtSecurityToken token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HashPassword(string username, string password)
    {
        string hashedPassword = _hasher.HashPassword(username, password);
        return hashedPassword;
    }

    public bool VerifyPassword(string username, string hashedPassword, string password)
    {
        PasswordVerificationResult result = _hasher.VerifyHashedPassword(username, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}