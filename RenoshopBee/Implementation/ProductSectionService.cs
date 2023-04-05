using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation
{
    public class ProductSectionService:IProductSection
    {
        private readonly ApplicationDbContext _context;
        public ProductSectionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetSectionProductsAsync(string section, string? sort, int pageNumber)
        {
            var products = _context.Products.Where(p => p.Category == section);
            if (!string.IsNullOrEmpty(sort))
            {
                var sortStrategy = GetSortStrategy(sort);
                products = sortStrategy.SortTechnique(products);
            }
            var productsPage = products.Skip((pageNumber - 1) * 10).Take(10);
            return await productsPage.ToListAsync();
        }
        private IProductSortTechnique GetSortStrategy(string sort)
        {
            switch (sort)
            {
                case "Price":
                    return new SortByPrice();
                case "Modified":
                    return new SortByModifiedDate();
                default:
                    return null;
            }
        }
    }
}
