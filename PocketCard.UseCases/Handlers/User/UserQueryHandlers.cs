using MediatR;
using Microsoft.AspNetCore.Http;

public class GetuserByUsernameHandler : IRequestHandler<GetuserByUsernameQuery, IResult>
{
    private readonly IUserReadRepository _repository;
    public GetuserByUsernameHandler(IUserReadRepository repository)
    {
        _repository = repository;
    }
    public async Task<IResult> Handle(GetuserByUsernameQuery request, CancellationToken token)
    {
        User? user = await _repository.GetuserByUsername(request.username, token);
        if (user is null) return Results.NotFound("User not found");
        UserQueryResponse response = new UserQueryResponse
        {
            id = user.Id,
            name = user.Name,
            email = user.Email
        };
        return Results.Ok(response);
    }
}




public class GetUserByIdHandler(IUserReadRepository repository) : IRequestHandler<GetUserByIdQuery, UserQueryResponse>
{
    public async Task<UserQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await repository.GetUserById(request.UserId, cancellationToken);
        UserQueryResponse response = new UserQueryResponse
        {
            id = user.Id,
            name = user.Name,
            email = user.Email
        };
        return response;
    }
}