using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.ProductServices
{
    public class ProductContextServices : IProductContext
    {
        private readonly ApplicationDbContext _context;
        public ProductContextServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(product => product.ID == productId); ;
        }
        public Product GetProductById(int productId)
        {
            return  _context.Products.FirstOrDefault(product => product.ID == productId); ;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
