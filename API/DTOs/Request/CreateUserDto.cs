using API.Enums;

namespace API.DTOs.Request;

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole? Role { get; set; }
}