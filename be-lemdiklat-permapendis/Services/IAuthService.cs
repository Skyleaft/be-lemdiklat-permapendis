using be_lemdiklat_permapendis.Dto;
using LoginRequest = be_lemdiklat_permapendis.Dto.LoginRequest;

namespace be_lemdiklat_permapendis.Services;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest request);
    Task<ServiceResponse> Register(RegisterRequest request);
    Task<ServiceResponse> Logout();
    Task<ServiceResponse> ChangePassword(ChangePasswordRequest request);
    Task<ServiceResponse> ForgotPassword(string email);
    Task<ServiceResponse> ChangeForgotPassword(ChangeForgotPasswordRequest request);
}