using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Services;

public interface IArticleService
{
    Task<Article> Get(int id);
    Task<ServiceResponse> Create(CreateArticleDto articleDto);
    Task<ServiceResponse> Update(int id, UpdateArticleDto articleDto);
    Task<ServiceResponse> Delete(int id);
    Task<PaginatedResponse<Article>> Find(FindRequest request);
}
