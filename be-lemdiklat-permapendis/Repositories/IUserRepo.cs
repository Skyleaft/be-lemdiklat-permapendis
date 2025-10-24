using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Repositories;

public interface IUserRepo
{
    Task<User> Get(Guid id);
    Task<User> Get(string username);
    Task<User> Create(User user);
    Task<User> Update(User user);
    Task Delete(Guid id);
    Task<IEnumerable<User>> GetAll();
}