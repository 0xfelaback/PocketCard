namespace PocketCard.UseCases;

public interface ICardRepository
{
    Task<Card?> GetCardByIdAsync(int id);
    Task CreateCardAsync(Card card);
    void Update(Card card);
    void Delete(Card card);
    Task SaveChangesAsync();
}
