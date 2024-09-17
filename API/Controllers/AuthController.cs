using API.Data;
using API.DTOs.Request;
using API.DTOs.Response;
using API.Helpers;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        AuthTokenDto authToken = await _authService.Login(model);
        return Ok(authToken);
    }
}