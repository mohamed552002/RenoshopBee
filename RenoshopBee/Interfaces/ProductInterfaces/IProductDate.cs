using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductDate
    {
        public void SetProductDatesToNow(Product product);
        public void SetProductUpdatedAtNow(Product product);
    }
}
