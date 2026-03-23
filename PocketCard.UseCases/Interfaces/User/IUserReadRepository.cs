public interface IUserReadRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id, CancellationToken token);
    Task<User?> GetuserByUsername(string username, CancellationToken token);
}