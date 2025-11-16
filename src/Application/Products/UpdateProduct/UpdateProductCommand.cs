using Domain.Entities.Products;
using Domain.Interfaces.Products;
using Domain.Interfaces.UnitOfWork;
using Domain.Shared;
using MediatR;

namespace Application.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string? Description)
    : IRequest<Result<UpdateProductResult>>;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Result<UpdateProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateProductResult>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<UpdateProductResult>(ProductErrors.NotFound(request.Id));
        }

        var updateResult = product.Update(request.Name, request.Description);

        if (updateResult.IsFailure)
            return Result.Failure<UpdateProductResult>(updateResult.Error);

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(UpdateProductResult.FromEntity(product));
    }
}