using Microsoft.EntityFrameworkCore;
using PocketCard.UseCases;

namespace PocketCard.Infrastructure.Repositories;

public class AccountRepository(PocketcardDbContext context) : IAccountRepository
{
    public async Task CreateAccount(Account account) => await context.Accounts.AddAsync(account);
    public void Update(Account account) => context.Accounts.Update(account);
    public void Delete(Account account) => context.Accounts.Remove(account);
    public async Task SaveChangesAsync(CancellationToken? token) => await context.SaveChangesAsync(token.GetValueOrDefault());
}
