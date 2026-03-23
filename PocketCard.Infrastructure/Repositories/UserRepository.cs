using Microsoft.EntityFrameworkCore;
using PocketCard.UseCases;

namespace PocketCard.Infrastructure.Repositories;

public class UserRepository(PocketcardDbContext context) : IUserRepository
{
    public async Task CreateUser(User user) => await context.Users.AddAsync(user);
    public void Update(User user) => context.Users.Update(user);
    public void Delete(User user) => context.Users.Remove(user);
    public async Task SaveChangesAsync(CancellationToken? token) => await context.SaveChangesAsync(token.GetValueOrDefault());
}
