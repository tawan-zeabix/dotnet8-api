using API.Data;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories;

public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
        
    }
}