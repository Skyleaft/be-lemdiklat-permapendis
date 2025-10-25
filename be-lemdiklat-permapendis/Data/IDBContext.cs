using be_lemdiklat_permapendis.Models;
using Microsoft.EntityFrameworkCore;

namespace be_lemdiklat_permapendis.Data;

public interface IDBContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserProfile> UserProfiles { get; set; }
    DbSet<Article>Articles { get; set; }
    DbSet<ArticleCategory> ArticleCategories { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}