using Domain.Entities.Products;
using Domain.Interfaces.Products;
using Infra.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(Product product)
    {
        _dbContext.Products.Add(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var productId = new ProductId(id);

        return await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }
}