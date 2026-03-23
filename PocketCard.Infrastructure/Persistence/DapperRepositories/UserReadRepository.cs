using System.Data;
using Dapper;


public class UserReadRepository(IDbConnection dbConnection) : IUserReadRepository
{
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await dbConnection.QueryAsync<User>("SELCET * FROM Users");
        return users.ToList();
    }
    public async Task<User?> GetUserById(int id, CancellationToken token) => await dbConnection.QueryFirstOrDefaultAsync<User>(new CommandDefinition("SELECT * from Users WHERE Id = @id", new { id }, cancellationToken: token));

    public async Task<User?> GetuserByUsername(string username, CancellationToken token) => await dbConnection.QueryFirstOrDefaultAsync<User>(new CommandDefinition("SELECT * from Users WHERE Name = @username", new { username }, cancellationToken: token));
}

