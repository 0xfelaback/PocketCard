using Microsoft.EntityFrameworkCore;
using PocketCard.UseCases;

namespace PocketCard.Infrastructure;

public class CardRepository(PocketcardDbContext context) : ICardRepository
{
    public async Task<Card?> GetCardByIdAsync(int id) => await context.Cards.FirstOrDefaultAsync(c => c.Id == id);
    public async Task CreateCardAsync(Card card) => await context.Cards.AddAsync(card);
    public void Update(Card card) => context.Cards.Update(card);
    public void Delete(Card card) => context.Cards.Remove(card);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
