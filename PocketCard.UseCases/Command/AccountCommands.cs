using MediatR;
using Microsoft.AspNetCore.Http;

public record CreateAccountCommand(int UserId, string AccountName, string AccountType, decimal Balance) : IRequest<IResult>;

public record GetAccountByIdQuery(int AccountId) : IRequest<IResult>;

public record GetAccountsByUserIdQuery(int UserId) : IRequest<IResult>;

public record UpdateAccountCommand(int AccountId, string? AccountName, decimal? Balance) : IRequest<IResult>;

public record DeleteAccountCommand(int AccountId) : IRequest<IResult>;
