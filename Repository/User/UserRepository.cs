using Microsoft.EntityFrameworkCore;
using webapitest.Controllers.Models;
using webapitest.Data;
using webapitest.Repository.Interfaces;
using webapitest.Repository.Models;

namespace webapitest.Repository;

public class UserRepository : IUserRepository
{
    private readonly PostgresDbContext _dbContext;

    public UserRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<UserModel> GetUserById(Guid userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task<UserModel> FindUser(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<string> Register(CreateUserModel request)
    {
        var newUser = new UserModel
        {
            Email = request.Email,
            Password = request.Password
        };

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return newUser.Id.ToString();
    }
}