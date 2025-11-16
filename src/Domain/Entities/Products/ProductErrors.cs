using Domain.Shared;

namespace Domain.Entities.Products;

public static class ProductErrors
{
    public static Error NotFound(Guid productId) => Error.NotFound(
        "Product.NotFound",
        $"Produto com ID '{productId}' não foi encontrado.");

    public static Error NameEmpty => Error.Validation(
        "Product.Name.Empty",
        "O nome do produto não pode ser vazio.");

    public static Error NameTooLong => Error.Validation(
        "Product.Name.TooLong",
        "O nome do produto não pode exceder 200 caracteres.");
}