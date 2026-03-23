using System.ComponentModel.DataAnnotations;

public record CreateCardRequestDTO
{
    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Account ID is required")]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "Card type is required")]
    public string CardType { get; set; } = null!;

    [Required(ErrorMessage = "Expiration date is required")]
    public DateOnly ExpirationDate { get; set; }

    [Required(ErrorMessage = "CVV is required")]
    [Range(100, 9999)]
    public int CVV { get; set; }

    [Required(ErrorMessage = "Card number is required")]
    [Length(13, 19)]
    public string CardNumber { get; set; } = null!;

    [Required(ErrorMessage = "Card network is required")]
    public string Network { get; set; } = null!;

    public bool Contactless { get; set; } = false;

    public bool InternationalUsage { get; set; } = false;
}

public record UpdateCardRequestDTO
{
    public bool? Contactless { get; set; }
    public bool? InternationalUsage { get; set; }
}

public record GetCardByIdRequestDTO
{
    [Required(ErrorMessage = "Card ID is required")]
    public int CardId { get; set; }
}
