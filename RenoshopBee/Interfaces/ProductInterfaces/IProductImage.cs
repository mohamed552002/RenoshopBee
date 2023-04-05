using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductImage
    {
        public void ProductImageSet(Product product, IFormFile? imgFile);
        public string ProductImageGet(Product product);
        public void  ProductImageDelete(Product product);
        public void ProductImageEdit(Product product,IFormFile? formFile);

    }
}
