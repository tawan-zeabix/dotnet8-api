using API.DTOs.Request;
using API.DTOs.Response;

namespace API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthTokenDto> Login(LoginDto model);
}