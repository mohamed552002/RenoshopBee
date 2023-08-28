using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NuGet.LibraryModel;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;
using System.Collections;

namespace RenoshopBee.Implementation.ProductServices
{
    public class ProductSizeServices : IProductSizes
    {
        private readonly ApplicationDbContext _context;
        public ProductSizeServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart GetAllCartProductSizes(Cart cart)
        {
            cart.products.ForEach(product => product.availableSizes = GetProductSizes(product));
            return cart;
        }

        public List<ProductSizes> GetProductSizes(Product product)
        {
           return _context.productSizes.Where(p => p.ProductId == product.ID).ToList();
        }

        public void SetProductSizes(Product product, List<string> sizes)
        {
            product.availableSizes = sizes.Select(size => new ProductSizes { ProductId = product.ID, Size = size }).ToList();
        }
    }
}
