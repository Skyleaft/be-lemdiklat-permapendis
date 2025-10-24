using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Services;

public interface IUserService
{
    Task<User> Get(Guid id);
    Task Create(CreateUserRequest request);
    Task Update(Guid id,UserProfile profile);
    Task Delete(Guid id);
    Task<User> ValidateUser(string username, string password);
    Task<User> GetUserByEmail(string email);
    Task<IEnumerable<User>> Find(FindRequest findRequest);
}