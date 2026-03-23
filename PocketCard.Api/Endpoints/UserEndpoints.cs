using MediatR;

using PocketCard.UseCases;

public static class UserEndpoints
{
    public static void UseUserEndpoints(this WebApplication app)
    {
        /*app.MapGet("/api/users/{id}", async (int id, IMediator mediator) =>
        {
            var query = new GetUserByIdQuery(id);
            return await mediator.Send(query);
        }).WithName("GetUserById");*/
        app.MapGet("/api/users/{username}", async (string username, ISender sender) =>
        {
            var query = new GetuserByUsernameQuery(username);
            return await sender.Send(query);
        }).WithName("GetUserByUsername");

        app.MapPost("/api/users/register", async (CreateUserRequestDTO request, ISender sender) =>
        {
            var query = new CreateUserCommand(request.name, request.password, request.email);
            return await sender.Send(query);
        });

        app.MapPost("/api/users/login", async (LoginUserRequestDTO request, ISender sender) =>
        {
            var query = new LoginUserCommand(request.name, request.password);
            return await sender.Send(query);
        });
    }
}












/*

// 1. The Notification (The Event)
public record UserRegisteredNotification(int UserId, string Email) : INotification;

// 2. Listener A (Email)
public class SendWelcomeEmailHandler : INotificationHandler<UserRegisteredNotification>
{
    public async Task Handle(UserRegisteredNotification notification, CancellationToken ct)
    {
        // Code to send email...
    }
}

// 3. Listener B (Audit)
public class AuditRegistrationHandler : INotificationHandler<UserRegisteredNotification>
{
    public async Task Handle(UserRegisteredNotification notification, CancellationToken ct)
    {
        // Code to log to DB...
    }
}


//Triggered with: await _mediator.Publish(new UserRegisteredNotification(user.Id, user.Email));

*/