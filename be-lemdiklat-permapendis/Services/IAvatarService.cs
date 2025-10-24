namespace be_lemdiklat_permapendis.Services;

public interface IAvatarService
{
    Task<string> GenerateAvatarAsync(string name, Guid userId);
    void DeleteOldProfilePhoto(string? fileName);
    Task<string> CompressToWebPAsync(IFormFile file, Guid userId);
}