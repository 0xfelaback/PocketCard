using MediatR;
using Microsoft.AspNetCore.Http;

public record GetCardsForUserResponse
{
    public List<CardResponse> Cards { get; set; } = null!;
}

public record CardResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AccountId { get; set; }
    public string Type { get; set; } = null!;
    public DateOnly ExpirationDate { get; set; }
    public int CVV { get; set; }
    public string CardNumber { get; set; } = null!;
    public string Network { get; set; } = null!;
    public bool Contactless { get; set; }
    public bool InternationalUsage { get; set; }
}