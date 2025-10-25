using be_lemdiklat_permapendis.Data;
using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;
using Microsoft.EntityFrameworkCore;

namespace be_lemdiklat_permapendis.Repositories;

public class UserRepo(IDBContext db) : IUserRepo
{
    public async Task<User> Get(Guid id)
    {
        var user = await db.Users.Include(x=>x.Role).Include(x=>x.UserProfile)
            .FirstOrDefaultAsync(x=>x.Id==id);
        if (user == null)
        {
            return null;
        }
        else
        {
            return user;
        }
    }

    public async Task<User> Get(string username)
    {
        var user = await db.Users.Include(x=>x.UserProfile).FirstOrDefaultAsync(x => x.Username == username);
        if (user == null)
        {
            return null;
        }
        else
        {
            return user;
        }
    }

    public async Task<User> Create(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(User user)
    {
        var existingUser = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (existingUser == null)
        {
            return null;
        }
        else
        {
            existingUser.UserProfile = user.UserProfile;
            await db.SaveChangesAsync();
            return existingUser;
        }
    }

    public async Task Delete(Guid id)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return;
        }
        else
        {
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }
    }

    public async Task<PaginatedResponse<User>> Find(FindRequest request)
    {
        var query = db.Users.Include(x => x.UserProfile).AsQueryable();
        
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(x => x.Username.Contains(request.Search) || 
                                   x.UserProfile.Name.Contains(request.Search) ||
                                   x.UserProfile.Email.Contains(request.Search));
        }
        
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
        return new PaginatedResponse<User>
        {
            Data = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}