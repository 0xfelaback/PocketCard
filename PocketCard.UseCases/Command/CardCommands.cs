using MediatR;
using Microsoft.AspNetCore.Http;

public record CreateCardCommand(int UserId, int AccountId, string CardType, DateOnly ExpirationDate, int CVV, string CardNumber, string Network, bool Contactless, bool InternationalUsage) : IRequest<IResult>;

public record UpdateCardCommand(int CardId, bool? Contactless, bool? InternationalUsage) : IRequest<IResult>;

public record DeleteCardCommand(int CardId) : IRequest<IResult>;

public record GetCardByIdQuery(int CardId) : IRequest<IResult>;

public record GetCardsForUserQuery(int UserId) : IRequest<IResult>;

public record GetCardsForAccountQuery(int AccountId) : IRequest<IResult>;

public record TryUseCardQuery(int CardId) : IRequest<string>;