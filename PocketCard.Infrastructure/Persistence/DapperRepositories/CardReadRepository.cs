using System.Data;
using Dapper;

public class CardReadRepository(IDbConnection dbConnection) : ICardReadRepository
{
    public async Task<CardResponse?> GetCardById(int id, CancellationToken token)
    {
        var card = await dbConnection.QueryFirstOrDefaultAsync<CardResponse>(
            new CommandDefinition("SELECT Id, UserId, AccountID as AccountId, Type, ExpirationDate, CVV, CardNumber, Network, Contactless, InternationalUsage FROM Cards WHERE Id = @id",
                new { id },
                cancellationToken: token));
        return card;
    }

    public async Task<List<CardResponse>?> GetCardsByUserId(int userId, CancellationToken token)
    {
        var cards = await dbConnection.QueryAsync<CardResponse>(
            new CommandDefinition("SELECT Id, UserId, AccountID as AccountId, Type, ExpirationDate, CVV, CardNumber, Network, Contactless, InternationalUsage FROM Cards WHERE UserId = @userId",
                new { userId },
                cancellationToken: token));
        return cards.ToList();
    }

    public async Task<List<CardResponse>?> GetCardsByAccountId(int accountId, CancellationToken token)
    {
        var cards = await dbConnection.QueryAsync<CardResponse>(
            new CommandDefinition("SELECT Id, UserId, AccountID as AccountId, Type, ExpirationDate, CVV, CardNumber, Network, Contactless, InternationalUsage FROM Cards WHERE AccountID = @accountId",
                new { accountId },
                cancellationToken: token));
        return cards.ToList();
    }
}
