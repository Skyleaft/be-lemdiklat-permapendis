using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;
using be_lemdiklat_permapendis.Repositories;

namespace be_lemdiklat_permapendis.Services;

public class ArticleCategoryService(IArticleCategoryRepo categoryRepo) : IArticleCategoryService
{
    public async Task<ArticleCategory> Get(int id)
    {
        return await categoryRepo.Get(id);
    }

    public async Task<ServiceResponse> Create(CreateArticleCategoryDto categoryDto)
    {
        try
        {
            var category = new ArticleCategory
            {
                Name = categoryDto.Name
            };

            await categoryRepo.Create(category);
            return new ServiceResponse { Success = true, Message = "Category created successfully" };
        }
        catch (Exception ex)
        {
            return new ServiceResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<ServiceResponse> Update(int id, UpdateArticleCategoryDto categoryDto)
    {
        try
        {
            var existing = await categoryRepo.Get(id);
            if (existing == null)
            {
                return new ServiceResponse { Success = false, Message = "Category not found" };
            }

            existing.Name = categoryDto.Name;
            await categoryRepo.Update(existing);
            return new ServiceResponse { Success = true, Message = "Category updated successfully" };
        }
        catch (Exception ex)
        {
            return new ServiceResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<ServiceResponse> Delete(int id)
    {
        try
        {
            var existing = await categoryRepo.Get(id);
            if (existing == null)
            {
                return new ServiceResponse { Success = false, Message = "Category not found" };
            }

            await categoryRepo.Delete(id);
            return new ServiceResponse { Success = true, Message = "Category deleted successfully" };
        }
        catch (Exception ex)
        {
            return new ServiceResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<PaginatedResponse<ArticleCategory>> Find(FindRequest request)
    {
        return await categoryRepo.Find(request);
    }

    public async Task<IEnumerable<ArticleCategory>> GetAll()
    {
        return await categoryRepo.GetAll();
    }
}