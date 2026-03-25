using MediatR;
using Microsoft.AspNetCore.Http;

namespace PocketCard.UseCases.Handlers.Card;

public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, IResult>
{
    private readonly ICardReadRepository _repository;

    public GetCardByIdQueryHandler(ICardReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var card = await _repository.GetCardById(request.CardId, cancellationToken);
        if (card is null) return Results.NotFound("Card not found");
        return Results.Ok(card);
    }
}

public class GetCardsForUserQueryHandler : IRequestHandler<GetCardsForUserQuery, IResult>
{
    private readonly ICardReadRepository _repository;

    public GetCardsForUserQueryHandler(ICardReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(GetCardsForUserQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.GetCardsByUserId(request.UserId, cancellationToken);
        if (cards is null || cards.Count == 0) return Results.NotFound("No cards found for user");

        var response = new GetCardsForUserResponse
        {
            Cards = cards
        };
        return Results.Ok(response);
    }
}

public class GetCardsForAccountQueryHandler : IRequestHandler<GetCardsForAccountQuery, IResult>
{
    private readonly ICardReadRepository _repository;

    public GetCardsForAccountQueryHandler(ICardReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(GetCardsForAccountQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.GetCardsByAccountId(request.AccountId, cancellationToken);
        if (cards is null || cards.Count == 0) return Results.NotFound("No cards found for account");

        var response = new GetCardsForUserResponse
        {
            Cards = cards
        };
        return Results.Ok(response);
    }
}

public class TryUseCardQueryHandler : IRequestHandler<TryUseCardQuery, string>
{
    private readonly ICardReadRepository _repository;

    public TryUseCardQueryHandler(ICardReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(TryUseCardQuery request, CancellationToken cancellationToken)
    {
        var card = await _repository.GetCardById(request.CardId, cancellationToken);
        if (card is null) return "Card not found";
        //DateOnly.TryParse(card.ExpirationDate.ToString(), out DateOnly parsedDate);
        //DateTime.TryParseExact(, "yyyy-MM-dd", out DateTime parsedDate);
        /*DateOnly parsedDate = DateOnly.ParseExact(
            card.ExpirationDate.ToString(), 
            "yyyy-MM-dd", 
            System.Globalization.CultureInfo.InvariantCulture
        );*/
        if (card.ExpirationDate < DateOnly.FromDateTime(DateTime.Now)) return "Card is expired";
        return "Card is valid";
    }
}