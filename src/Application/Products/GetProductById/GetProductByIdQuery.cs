using Domain.Interfaces.Products;
using Domain.Shared;
using MediatR;

namespace Application.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<GetProductByIdResult>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResult>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<GetProductByIdResult>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<GetProductByIdResult>(
                new Error(
                    "Product.NotFound",
                    $"Produto com ID '{request.Id}' não foi encontrado"));
        }

        return Result.Success(GetProductByIdResult.FromEntity(product));
    }
}