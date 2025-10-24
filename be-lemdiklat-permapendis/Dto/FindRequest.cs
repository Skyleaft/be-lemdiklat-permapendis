namespace be_lemdiklat_permapendis.Dto;

public class FindRequest
{
    public string Search { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public string Sort { get; set; } = string.Empty;
    public string Order { get; set; } = string.Empty;
    public string Filter { get; set; } = string.Empty;
}