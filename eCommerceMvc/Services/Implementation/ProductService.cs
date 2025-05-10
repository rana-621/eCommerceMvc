using eCommerceMvc.Data;
using eCommerceMvc.Models;
using eCommerceMvc.Services.Interfaces;

namespace eCommerceMvc.Services.Implementation;

public class ProductService : IProductInterface
{

    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync()
    {
        throw new NotImplementedException();
    }
    public Task CreateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
