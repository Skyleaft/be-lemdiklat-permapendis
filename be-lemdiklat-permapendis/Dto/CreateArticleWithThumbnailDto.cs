namespace be_lemdiklat_permapendis.Dto;

public class CreateArticleWithThumbnailDto : CreateArticleDto
{
    public IFormFile? ThumbnailFile { get; set; }
}