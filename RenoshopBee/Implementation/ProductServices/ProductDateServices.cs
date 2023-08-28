using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.ProductServices
{
    public class ProductDateServices : IProductDate
    {
        public void SetProductDatesToNow(Product product)
        {
            product.Created_at = DateTime.Now;
            product.Last_updated_at = DateTime.Now;
        }


        public void SetProductUpdatedAtNow(Product product)
        {
            product.Last_updated_at = DateTime.Now;
        }
    }
}
