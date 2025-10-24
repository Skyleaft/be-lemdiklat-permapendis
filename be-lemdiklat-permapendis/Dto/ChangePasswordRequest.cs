namespace be_lemdiklat_permapendis.Dto;

public class ChangePasswordRequest
{
    public string OldPassword { get; set; }=string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}