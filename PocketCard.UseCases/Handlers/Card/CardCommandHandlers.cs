using MediatR;
using Microsoft.AspNetCore.Http;
using PocketCard.UseCases;

public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, IResult>
{
    private readonly ICardRepository _repository;

    public CreateCardCommandHandler(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var newCard = new Card
        {
            UserId = request.UserId,
            AccountID = request.AccountId,
            Type = (Card.cardType)Enum.Parse(typeof(Card.cardType), request.CardType),
            ExpirationDate = request.ExpirationDate,
            CVV = request.CVV,
            CardNumber = request.CardNumber,
            Network = (Card.cardNetwork)Enum.Parse(typeof(Card.cardNetwork), request.Network),
            Contactless = request.Contactless,
            InternationalUsage = request.InternationalUsage
        };

        await _repository.CreateCardAsync(newCard);
        await _repository.SaveChangesAsync();
        return Results.Created();
    }
}

public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand, IResult>
{
    private readonly ICardRepository _repository;
    private readonly ICardReadRepository _readRepository;

    public UpdateCardCommandHandler(ICardRepository repository, ICardReadRepository readRepository)
    {
        _repository = repository;
        _readRepository = readRepository;
    }

    public async Task<IResult> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        var card = await _repository.GetCardByIdAsync(request.CardId);
        if (card is null) return Results.NotFound("Card not found");

        if (request.Contactless.HasValue)
            card.Contactless = request.Contactless.Value;

        if (request.InternationalUsage.HasValue)
            card.InternationalUsage = request.InternationalUsage.Value;

        _repository.Update(card);
        await _repository.SaveChangesAsync();
        
        var cardResponse = await _readRepository.GetCardById(request.CardId, cancellationToken);
        return Results.Ok(cardResponse);
    }
}

public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, IResult>
{
    private readonly ICardRepository _repository;

    public DeleteCardCommandHandler(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        var card = await _repository.GetCardByIdAsync(request.CardId);
        if (card is null) return Results.NotFound("Card not found");

        _repository.Delete(card);
        await _repository.SaveChangesAsync();
        return Results.NoContent();
    }
}