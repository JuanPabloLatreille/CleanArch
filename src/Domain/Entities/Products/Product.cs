using Domain.Shared;

namespace Domain.Entities.Products;

public sealed class Product
{
    private Product()
    {
    }

    private Product(string name, string? description)
    {
        Id = new ProductId(Guid.NewGuid());
        Name = name;
        Description = description;
    }

    public ProductId Id { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public static Result<Product> Create(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Product>(
                new Error("Product.Name.Empty", "O nome do produto é obrigatório"));

        if (name.Length > 200)
            return Result.Failure<Product>(
                new Error("Product.Name.TooLong", "O nome não pode exceder 200 caracteres"));

        var product = new Product(name, description);
        return Result.Success(product);
    }

    public Result Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(
                new Error("Product.Name.Empty", "O nome do produto é obrigatório"));

        if (name.Length > 200)
            return Result.Failure(
                new Error("Product.Name.TooLong", "O nome não pode exceder 200 caracteres"));

        Name = name;
        Description = description;

        return Result.Success();
    }
}