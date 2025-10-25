using be_lemdiklat_permapendis.Data;
using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;
using Microsoft.EntityFrameworkCore;

namespace be_lemdiklat_permapendis.Repositories;

public class ArticleRepo(IDBContext db) : IArticleRepo
{
    public async Task<Article> Get(int id)
    {
        return await db.Articles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Article> Create(Article article)
    {
        await db.Articles.AddAsync(article);
        await db.SaveChangesAsync();
        return article;
    }

    public async Task<Article> Update(Article article)
    {
        var existingArticle = await db.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
        if (existingArticle == null)
        {
            return null;
        }
        
        existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        existingArticle.CategoryId = article.CategoryId;
        existingArticle.Thumbnail = article.Thumbnail;
        existingArticle.UpdatedAt = DateTime.UtcNow;
        
        await db.SaveChangesAsync();
        return existingArticle;
    }

    public async Task Delete(int id)
    {
        var article = await db.Articles.FirstOrDefaultAsync(x => x.Id == id);
        if (article != null)
        {
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
        }
    }

    public async Task<PaginatedResponse<Article>> Find(FindRequest request)
    {
        var query = db.Articles.AsQueryable();
        
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(x => x.Title.Contains(request.Search) || x.Content.Contains(request.Search));
        }
        
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
        return new PaginatedResponse<Article>
        {
            Data = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}