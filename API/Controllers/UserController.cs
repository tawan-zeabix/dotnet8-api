using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<UserModel>> GetAllUsers()
    {
        List<UserModel> users = await _userService.GetAllUsers();
        return users;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        UserModel user = await _userService.GetUserById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task CreateUser(UserModel user)
    {
        await _userService.CreateUser(user);
    }
}