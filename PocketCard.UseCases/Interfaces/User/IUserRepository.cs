namespace PocketCard.UseCases;

public interface IUserRepository
{
    Task CreateUser(User user);
    void Update(User user);
    void Delete(User user);
    Task SaveChangesAsync(CancellationToken? token);
}
