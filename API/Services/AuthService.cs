using System.Security.Cryptography;
using System.Text;
using API.DTOs.Request;
using API.DTOs.Response;
using API.Helpers;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly AuthHelper _authHelper;
    
    public AuthService(IUserRepository userRepository, AuthHelper authHelper)
    {
        _userRepository = userRepository;
        _authHelper = authHelper;
    }

    public async Task<AuthTokenDto> Login(LoginDto model)
    {
        UserModel? user = await _userRepository.GetQueryable()
            .Where(i => i.Username == model.Username)
            .SingleOrDefaultAsync();
        if (user is null || !_authHelper.VerifyPassword(model.Username, user.Password, model.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        string accessToken = _authHelper.GenerateToken(user);
        return new AuthTokenDto() { AccessToken = accessToken };
    }
}