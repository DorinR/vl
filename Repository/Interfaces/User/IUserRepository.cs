using webapitest.Controllers.Models;
using webapitest.Repository.Models;

namespace webapitest.Repository.Interfaces;

public interface IUserRepository
{
    public Task<User> GetUserById(Guid userId);

    public Task<User> FindUser(string email);

    public Task<string> Register(CreateUserModel model);
}