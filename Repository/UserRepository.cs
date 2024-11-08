using Microsoft.EntityFrameworkCore;
using webapitest.Controllers.Models;
using webapitest.Data;
using webapitest.Repository.Models;
using webapitest.Repository.Interfaces;

namespace webapitest.Repository;

public class UserRepository : IUserRepository
{
    private readonly PostgresDbContext _dbContext;

    public UserRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<User> GetUserById(Guid userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task<User> FindUser(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<string> Register(CreateUserModel request)
    {
        var newUser = new User()
        {
            Email = request.Email,
            Password = request.Password
        };

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return newUser.Id.ToString();
    }
}