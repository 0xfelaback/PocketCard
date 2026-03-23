using MediatR;

public static class AccountEndpoints
{
    public static void UseAccountEndpoints(this WebApplication app)
    {
        app.MapPost("/api/accounts", async (int userId, CreateAccountRequestDTO request, ISender sender) =>
        {
            var command = new CreateAccountCommand(userId, request.AccountName, request.AccountType, request.Balance);
            return await sender.Send(command);
        }).WithName("CreateAccount");

        app.MapGet("/api/accounts/{id}", async (int id, ISender sender) =>
        {
            var query = new GetAccountByIdQuery(id);
            return await sender.Send(query);
        }).WithName("GetAccountById");

        app.MapGet("/api/accounts/user/{userId}", async (int userId, ISender sender) =>
        {
            var query = new GetAccountsByUserIdQuery(userId);
            return await sender.Send(query);
        }).WithName("GetAccountsByUserId");

        app.MapPut("/api/accounts/{id}", async (int id, UpdateAccountRequestDTO request, ISender sender) =>
        {
            var command = new UpdateAccountCommand(id, request.AccountName, request.Balance);
            return await sender.Send(command);
        }).WithName("UpdateAccount");

        app.MapDelete("/api/accounts/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteAccountCommand(id);
            return await sender.Send(command);
        }).WithName("DeleteAccount");
    }
}
