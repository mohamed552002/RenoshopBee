using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductSection
    {
        public Task<IEnumerable<Product>> GetSectionProductsAsync(string section, string? sort, int pageNumber);
    }
}
