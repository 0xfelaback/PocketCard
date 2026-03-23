namespace PocketCard.UseCases;

public interface IAccountReadRepository
{
    Task<Account?> GetAccountById(int id, CancellationToken token);
    Task<IEnumerable<Account>> GetAccountsByUserId(int userId, CancellationToken token);
}
