using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;
using be_lemdiklat_permapendis.Repositories;

namespace be_lemdiklat_permapendis.Services;

public class UserService(IUserRepo userRepo, IEncryptionService encryptionService) : IUserService
{
    public  async Task<User> Get(Guid id)
    {
        return await userRepo.Get(id);
    }

    public async Task Create(CreateUserRequest request)
    {
        var salt = encryptionService.GenerateSalt();
        request.Password = encryptionService.HashPassword(request.Password, salt);
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            PasswordSalt = salt, 
            UserProfile = new()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City
            },
            RoleId = request.RoleId
        };
        await userRepo.Create(user);
    }

    public async Task Update(Guid id, UserProfile profile)
    {
        var currentUser = await userRepo.Get(id);
        if (currentUser is null)
        {
            throw new Exception("User not found");
        }
        currentUser.UserProfile = profile;
        await userRepo.Update(currentUser);
    }

    public async Task Delete(Guid id)
    {
        var currentUser = await userRepo.Get(id);
        if (currentUser is null)
        {
            throw new Exception("User not found");
        }
        await userRepo.Delete(id);
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        var user = await userRepo.Get(username);
        if (user is null)
        {
            throw new Exception("username or password is not valid");
        }
        if (user.Password != password)
        {
            throw new Exception("username or password is not valid");
        }
        return user;
    }

    public Task<User> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResponse<User>> Find(FindRequest findRequest)
    {
        return await userRepo.Find(findRequest);
    }

    public async Task UpdateProfilePhoto(Guid userId, string fileName)
    {
        var user = await userRepo.Get(userId);
        if (user == null) throw new Exception("User not found");
        
        if (user.UserProfile == null) user.UserProfile = new UserProfile();
        user.UserProfile.ProfilePicture = fileName;
        
        await userRepo.Update(user);
    }
}