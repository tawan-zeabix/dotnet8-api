using API.DTOs.Request;
using API.Models;

namespace API.Services.Interfaces;

public interface IUserService
{
    Task<List<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(int id);
    Task UpdateUser(int id, CreateUserDto user);
    Task CreateUser(CreateUserDto user);
    Task DeleteUser(int id);
}