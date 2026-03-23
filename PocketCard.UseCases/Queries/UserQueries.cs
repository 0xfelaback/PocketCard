using MediatR;
using Microsoft.AspNetCore.Http;

public record GetUserByIdQuery(int UserId) : IRequest<UserQueryResponse>; //return value = UserQueryResponse
public record GetuserByUsernameQuery(string username) : IRequest<IResult>;//IRequest<UserQueryResponse, IResult>;










