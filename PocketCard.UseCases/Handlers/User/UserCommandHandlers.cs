
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PocketCard.UseCases;
using ProjectManagementAPI.src.UseCases;



public class CreateUserCommandhandler : IRequestHandler<CreateUserCommand, IResult>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<string> _passwordHasher;
    public CreateUserCommandhandler(IUserRepository repository, IPasswordHasher<string> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }
    public async Task<IResult> Handle(CreateUserCommand request, CancellationToken token)
    {
        string passwordHash = _passwordHasher.HashPassword(string.Empty, request.hashedPassword);
        User newUser = new User
        {
            Name = request.name,
            Email = request.email,
            Password = passwordHash

        };
        await _repository.CreateUser(newUser);
        await _repository.SaveChangesAsync(token);
        //return Results.CreatedAtRoute("GetUserByUsername", new { newUser.Name }, newUser);
        return Results.Created();
    }
}


public class LoginUserCommandHandler(IUserReadRepository repository, IPasswordHasher<string> passwordHasher, ITokenProvider tokenProvider) : IRequestHandler<LoginUserCommand, IResult>
{
    public async Task<IResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await repository.GetuserByUsername(request.name, cancellationToken);
        if (user is null) return Results.NotFound("This user does not exist");
        PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(string.Empty, user.Password, request.password);
        if (verificationResult == PasswordVerificationResult.Failed) return Results.Unauthorized();

        string token = tokenProvider.Create(user.Id, user.Name);
        UserQueryResponse userData = new UserQueryResponse { id = user.Id, name = user.Name, email = user.Email };
        UserAuthResponse responseData = new UserAuthResponse { loginResult = userData, accessToken = token };
        return Results.Ok(responseData);
    }
}

public class UpdateUserEmailCommandHandler(IUserRepository repository, IUserReadRepository readRepository) : IRequestHandler<UpdateUserEmailCommand>
{
    public async Task Handle(UpdateUserEmailCommand request, CancellationToken token)
    {
        User? user = await readRepository.GetUserById(request.UserId, token);
        user.Email = request.email;
        repository.Update(user);
        await repository.SaveChangesAsync(token);
    }
}