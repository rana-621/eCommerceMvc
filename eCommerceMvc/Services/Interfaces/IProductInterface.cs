using eCommerceMvc.Models;

namespace eCommerceMvc.Services.Interfaces;

public interface IProductInterface
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}
