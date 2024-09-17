using API.Models;

namespace API.Services.Interfaces;

public interface IUserService
{
    Task<List<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(int id);
    Task UpdateUser(UserModel user);
    Task CreateUser(UserModel user);
    Task DeleteUser(int id);
}