using webapitest.Controllers.Models;
using webapitest.Repository.Models;

namespace webapitest.Repository.Interfaces;

public interface IUserRepository
{
    public Task<UserModel> GetUserById(Guid userId);

    public Task<UserModel> FindUser(string email);

    public Task<string> Register(CreateUserModel model);
}