using RenoshopBee.Models;
using System.Collections;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductSizes
    {
        public void SetProductSizes(Product product,List<string> sizes);
        public List<ProductSizes> GetProductSizes(Product product);
        public Cart GetAllCartProductSizes(Cart cart);
    }
}
