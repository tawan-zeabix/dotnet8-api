using API.DTOs.Request;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
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
    public async Task<IActionResult> CreateUser(CreateUserDto model)
    {
        await _userService.CreateUser(model);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto model)
    {
        await _userService.UpdateUser(id, model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }
}