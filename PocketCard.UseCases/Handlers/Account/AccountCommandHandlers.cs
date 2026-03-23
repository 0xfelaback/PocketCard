using MediatR;
using Microsoft.AspNetCore.Http;
using PocketCard.UseCases;


public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, IResult>
{
    private readonly IAccountRepository _repository;

    public CreateAccountCommandHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(CreateAccountCommand request, CancellationToken token)
    {
        var newAccount = new Account
        {
            UserId = request.UserId,
            AccountName = request.AccountName,
            AccountType = request.AccountType,
            Balance = request.Balance,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.CreateAccount(newAccount);
        await _repository.SaveChangesAsync(token);
        return Results.Created();
    }
}

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, IResult>
{
    private readonly IAccountRepository _repository;
    private readonly IAccountReadRepository _readRepository;

    public UpdateAccountCommandHandler(IAccountRepository repository, IAccountReadRepository readRepository)
    {
        _repository = repository;
        _readRepository = readRepository;
    }

    public async Task<IResult> Handle(UpdateAccountCommand request, CancellationToken token)
    {
        var account = await _readRepository.GetAccountById(request.AccountId, token);
        if (account is null) return Results.NotFound("Account not found");

        if (!string.IsNullOrEmpty(request.AccountName))
            account.AccountName = request.AccountName;

        if (request.Balance.HasValue)
            account.Balance = request.Balance.Value;

        account.UpdatedAt = DateTime.UtcNow;
        _repository.Update(account);
        await _repository.SaveChangesAsync(token);
        return Results.Ok(account);
    }
}

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, IResult>
{
    private readonly IAccountRepository _repository;
    private readonly IAccountReadRepository _readRepository;

    public DeleteAccountCommandHandler(IAccountRepository repository, IAccountReadRepository readRepository)
    {
        _repository = repository;
        _readRepository = readRepository;
    }

    public async Task<IResult> Handle(DeleteAccountCommand request, CancellationToken token)
    {
        var account = await _readRepository.GetAccountById(request.AccountId, token);
        if (account is null) return Results.NotFound("Account not found");

        _repository.Delete(account);
        await _repository.SaveChangesAsync(token);
        return Results.NoContent();
    }
}
