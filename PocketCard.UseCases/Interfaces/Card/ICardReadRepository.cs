public interface ICardReadRepository
{
    Task<CardResponse?> GetCardById(int id, CancellationToken token);
    Task<List<CardResponse>?> GetCardsByUserId(int userId, CancellationToken token);
    Task<List<CardResponse>?> GetCardsByAccountId(int accountId, CancellationToken token);
}