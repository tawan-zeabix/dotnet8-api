using API.Helpers;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

    public async Task CreateUser(UserModel user)
    {
        try
        {
            await _userRepository.CreateAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerErrorException(e.Message);
        }
    }

    public async Task UpdateUser(UserModel user)
    {
        try
        {
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
        try
        {
            UserModel? user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InternalServerErrorException(e.Message);
        }
    }
}