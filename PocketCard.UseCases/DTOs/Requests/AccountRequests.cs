using System.ComponentModel.DataAnnotations;

public record CreateAccountRequestDTO
{
    [Required(ErrorMessage = "Account name is required")]
    public string AccountName { get; set; } = null!;
    [Required(ErrorMessage = "Account type is required")]
    public string AccountType { get; set; } = null!;
    [Range(0, double.MaxValue, ErrorMessage = "Balance must be non-negative")]
    public decimal Balance { get; set; }
}

public record UpdateAccountRequestDTO
{
    public string? AccountName { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Balance must be non-negative")]
    public decimal? Balance { get; set; }
}

public record GetAccountByIdRequestDTO
{
    [Required(ErrorMessage = "Account ID is required")]
    public int AccountId { get; set; }
}
