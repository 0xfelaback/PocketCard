using MediatR;
using Microsoft.AspNetCore.Http;
using PocketCard.UseCases;

namespace PocketCard.UseCases.Handlers.Account;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, IResult>
{
    private readonly IAccountReadRepository _readRepository;

    public GetAccountByIdQueryHandler(IAccountReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IResult> Handle(GetAccountByIdQuery request, CancellationToken token)
    {
        var account = await _readRepository.GetAccountById(request.AccountId, token);
        if (account is null) return Results.NotFound("Account not found");
        return Results.Ok(account);
    }
}

public class GetAccountsByUserIdQueryHandler : IRequestHandler<GetAccountsByUserIdQuery, IResult>
{
    private readonly IAccountReadRepository _readRepository;

    public GetAccountsByUserIdQueryHandler(IAccountReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IResult> Handle(GetAccountsByUserIdQuery request, CancellationToken token)
    {
        var accounts = await _readRepository.GetAccountsByUserId(request.UserId, token);
        return Results.Ok(accounts);
    }
}
