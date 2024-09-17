using API.DTOs.Request;
using API.Enums;
using API.Helpers;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly AuthHelper _authHelper;
    public UserService(IUserRepository userRepository, AuthHelper authHelper)
    {
        _userRepository = userRepository;
        _authHelper = authHelper;
    }

    public async Task<List<UserModel>> GetAllUsers()
    {
        return await _userRepository.GetAsync();
    }

    public async Task<UserModel> GetUserById(int id)
    {
        UserModel? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    public async Task CreateUser(CreateUserDto model)
    {
        try
        {
            UserModel user = new UserModel()
            {
                Id = 0,
                Email = model.Email,
                Username = model.Username,
                Password = _authHelper.HashPassword(model.Password),
                Role = model.Role ?? UserRole.User
            };
            await _userRepository.CreateAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public async Task UpdateUser(int id, CreateUserDto model)
    {
        UserModel? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        try
        {
            user.Username = model.Username;
            user.Email = model.Email;
            user.Role = model.Role ?? UserRole.User;
            await _userRepository.UpdateAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public async Task DeleteUser(int id)
    {
        UserModel? user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        try
        {
            await _userRepository.DeleteAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerErrorException(e.Message);
        }
    }
}