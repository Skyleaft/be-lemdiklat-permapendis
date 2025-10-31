using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Repositories;

public interface IArticleRepo
{
    Task<Article> Get(int id);
    Task<Article> GetBySlug(string slug);
    Task<Article> Create(Article article);
    Task<Article> Update(Article article);
    Task Delete(int id);
    Task<PaginatedResponse<Article>> Find(FindRequest request);
}