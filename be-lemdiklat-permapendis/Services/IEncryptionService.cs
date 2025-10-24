namespace be_lemdiklat_permapendis.Services;

public interface IEncryptionService
{
    string GenerateSalt();
    string HashPassword(string password,string salt);
    bool VerifyPassword(string password, string hash);
}