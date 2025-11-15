using Domain.Entities.Products;

namespace Application.Products.GetProductById;

public sealed record GetProductByIdResult(
    Guid Id,
    string Name,
    string? Description)
{
    public static GetProductByIdResult FromEntity(Product product)
    {
        return new GetProductByIdResult(
            product.Id.Value,
            product.Name,
            product.Description
        );
    }
}