using System.Data;
using Dapper;
using PocketCard.UseCases;

public class AccountReadRepository(IDbConnection dbConnection) : IAccountReadRepository
{
    public async Task<Account?> GetAccountById(int id, CancellationToken token)
        => await dbConnection.QueryFirstOrDefaultAsync<Account>(
            new CommandDefinition("SELECT * FROM Accounts WHERE Id = @id", new { id }, cancellationToken: token));

    public async Task<IEnumerable<Account>> GetAccountsByUserId(int userId, CancellationToken token)
        => await dbConnection.QueryAsync<Account>(
            new CommandDefinition("SELECT * FROM Accounts WHERE UserId = @userId", new { userId }, cancellationToken: token));
}
