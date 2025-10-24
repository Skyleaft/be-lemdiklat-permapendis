namespace be_lemdiklat_permapendis.Dto;

public class ChangeForgotPasswordRequest
{
    public string NewPassword { get; set; } = string.Empty;
    public string ChangePasswordToken { get; set; } = string.Empty;
}