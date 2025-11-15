using Domain.Entities.Products;

namespace Application.Products.UpdateProduct;

public sealed record UpdateProductResult(
    Guid Id,
    string Name,
    string? Description)
{
    public static UpdateProductResult FromEntity(Product product)
    {
        return new UpdateProductResult(
            product.Id.Value,
            product.Name,
            product.Description
        );
    }
}