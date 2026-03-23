namespace PocketCard.UseCases;

public interface IAccountRepository
{
    Task CreateAccount(Account account);
    void Update(Account account);
    void Delete(Account account);
    Task SaveChangesAsync(CancellationToken? token);
}
