using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .Select(failure => Error.Validation(
                $"{failure.PropertyName}.{failure.ErrorCode}",
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return CreateValidationResult(errors);
        }

        return await next(cancellationToken);
    }

    private static TResponse CreateValidationResult(Error[] errors)
    {
        var validationError = errors.Length == 1
            ? errors[0]
            : new ValidationError(errors);

        if (typeof(TResponse) == typeof(Result))
        {
            return (Result.Failure(validationError) as TResponse)!;
        }

        var resultType = typeof(TResponse).GetGenericArguments()[0];
        var failureMethod = typeof(Result)
            .GetMethod(nameof(Result.Failure), 1, new[] { typeof(Error) })!
            .MakeGenericMethod(resultType);

        return (TResponse)failureMethod.Invoke(null, new object[] { validationError })!;
    }
}