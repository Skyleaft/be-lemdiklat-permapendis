using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Repositories;

public interface IArticleCategoryRepo
{
    Task<ArticleCategory> Get(int id);
    Task<ArticleCategory> Create(ArticleCategory category);
    Task<ArticleCategory> Update(ArticleCategory category);
    Task Delete(int id);
    Task<PaginatedResponse<ArticleCategory>> Find(FindRequest request);
    Task<IEnumerable<ArticleCategory>> GetAll();
}