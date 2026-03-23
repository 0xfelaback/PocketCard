using FluentValidation;
using MediatR;

public class VaidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
{
    //next is the Handler
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        //whenall used to manage multiple cards at once.
        var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        // If there are errors, throw a validation exception
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        return await next();
    }
}