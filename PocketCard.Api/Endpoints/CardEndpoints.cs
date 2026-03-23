using MediatR;

public static class CardEndpoints
{
    public static void UseCardEndpoints(this WebApplication app)
    {
        app.MapPost("/api/cards", async (CreateCardRequestDTO request, ISender sender) =>
        {
            var command = new CreateCardCommand(
                request.UserId,
                request.AccountId,
                request.CardType,
                request.ExpirationDate,
                request.CVV,
                request.CardNumber,
                request.Network,
                request.Contactless,
                request.InternationalUsage);
            return await sender.Send(command);
        }).WithName("CreateCard");

        app.MapGet("/api/cards/{id}", async (int id, ISender sender) =>
        {
            var query = new GetCardByIdQuery(id);
            return await sender.Send(query);
        }).WithName("GetCardById");

        app.MapGet("/api/cards/user/{userId}", async (int userId, ISender sender) =>
        {
            var query = new GetCardsForUserQuery(userId);
            return await sender.Send(query);
        }).WithName("GetCardsByUser");

        app.MapGet("/api/cards/account/{accountId}", async (int accountId, ISender sender) =>
        {
            var query = new GetCardsForAccountQuery(accountId);
            return await sender.Send(query);
        }).WithName("GetCardsByAccount");

        app.MapPut("/api/cards/{id}", async (int id, UpdateCardRequestDTO request, ISender sender) =>
        {
            var command = new UpdateCardCommand(id, request.Contactless, request.InternationalUsage);
            return await sender.Send(command);
        }).WithName("UpdateCard");

        app.MapDelete("/api/cards/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteCardCommand(id);
            return await sender.Send(command);
        }).WithName("DeleteCard");

        app.MapGet("/api/cards/validate/{id}", async (int id, ISender sender) =>
        {
            var query = new TryUseCardQuery(id);
            return await sender.Send(query);
        }).WithName("ValidateCard");
    }
}