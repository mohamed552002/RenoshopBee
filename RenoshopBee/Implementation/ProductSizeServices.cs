using Microsoft.AspNetCore.Hosting;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation
{
    public class ProductSizeServices :IProductSizes
    {
        public void GetProductSizes(Product product)
        {
            throw new NotImplementedException();
        }

        public void SetProductSizes(Product product, List<string> sizes)
        {
            product.availableSizes = sizes.Select(size => new ProductSizes { ProductId = product.ID, Size = size }).ToList();
        }
    }
}
