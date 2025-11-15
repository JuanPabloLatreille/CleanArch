using Domain.Entities.Products;
using Domain.Interfaces.Products;
using Domain.Interfaces.UnitOfWork;
using Domain.Shared;
using MediatR;

namespace Application.Products.CreateProduct;

public record CreateProductCommand(string Name, string? Description) : IRequest<Result<CreateProductResult>>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProductResult>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var productResult = Product.Create(request.Name, request.Description);

        if (productResult.IsFailure)
            return Result.Failure<CreateProductResult>(productResult.Error);

        _productRepository.Create(productResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(CreateProductResult.FromEntity(productResult.Value));
    }
}