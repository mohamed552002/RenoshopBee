using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductSizes
    {
        public void SetProductSizes(Product product,List<string> sizes);
        public void GetProductSizes(Product product);
    }
}
