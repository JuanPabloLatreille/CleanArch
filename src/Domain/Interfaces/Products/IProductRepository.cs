using Domain.Entities.Products;

namespace Domain.Interfaces.Products;

public interface IProductRepository
{
    void Create(Product product);

    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    void Update(Product product);

    void Delete(Product product);
}