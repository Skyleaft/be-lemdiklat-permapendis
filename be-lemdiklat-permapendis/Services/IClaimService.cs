using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Services;

public interface IClaimService
{
    string? GetUserId();
    string? GetUserRole();
    string? GetUsername();
    string? GetEmail();
    string? GetProfileName();
    ClaimUser GetClaimUser();
    bool IsInRole(int[] roleId);
}