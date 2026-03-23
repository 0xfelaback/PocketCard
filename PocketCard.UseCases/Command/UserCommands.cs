using MediatR;
using Microsoft.AspNetCore.Http;

public record CreateUserCommand(string name, string hashedPassword, string email) : IRequest<IResult>;

public record LoginUserCommand(string name, string password) : IRequest<IResult>;

public record UpdateUserEmailCommand(int UserId, string email) : IRequest; //void request