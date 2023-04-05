using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductContext
    {
       public Task<IEnumerable<Product>> GetProductsAsync();
       public IEnumerable<Product> GetProducts();
       public Task<Product> GetProductByIdAsync(int productId);
    }
}
