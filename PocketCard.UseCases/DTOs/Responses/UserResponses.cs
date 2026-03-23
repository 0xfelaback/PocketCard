
public record UserQueryResponse
{
    public int id { get; set; }
    public string name { get; set; } = null!;
    public string email { get; set; } = null!;
}

public record UserAuthResponse
{
    public UserQueryResponse? loginResult { get; set; }
    public string? accessToken { get; set; }
};

public record LoginResultDTO
{
    public long Id;
    public required string name { get; set; }
    public required string email { get; set; }
}