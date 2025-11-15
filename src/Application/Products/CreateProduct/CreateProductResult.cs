using Domain.Entities.Products;

namespace Application.Products.CreateProduct;

public sealed record CreateProductResult(
    Guid Id, 
    string Name, 
    string? Description)
{
    public static CreateProductResult FromEntity(Product product)
    {
        return new CreateProductResult(
            product.Id.Value,
            product.Name,
            product.Description
        );
    }
}