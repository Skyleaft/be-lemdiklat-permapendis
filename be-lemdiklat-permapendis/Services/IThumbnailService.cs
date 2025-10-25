namespace be_lemdiklat_permapendis.Services;

public interface IThumbnailService
{
    Task<string> CompressToWebPAsync(IFormFile file);
    void DeleteOldThumbnail(string? fileName);
}