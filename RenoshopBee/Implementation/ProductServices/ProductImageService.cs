using Microsoft.AspNetCore.Hosting;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.ProductServices
{
    public class ProductImageService : IProductImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void ProductImageDelete(Product product)
        {
            if (product.Img_url != @"\images\No_Image.png")
            {
                var imgOldPath = _webHostEnvironment.WebRootPath + product.Img_url;
                if (File.Exists(imgOldPath))
                {
                    File.Delete(imgOldPath);
                }
            }
        }

        public void ProductImageEdit(Product product, IFormFile? formFile)
        {
            if (formFile != null)
            {
                ProductImageDelete(product);
                ProductImageSet(product, formFile);
            }
        }

        public string ProductImageGet(Product product)
        {
            return product.Img_url;
        }

        public void ProductImageSet(Product product, IFormFile? imgFile)
        {
            if (imgFile == null)
            {
                product.Img_url = "\\images\\No_Image.png";
            }
            else
            {
                string imgExtension = Path.GetExtension(imgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                product.Img_url = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    imgFile.CopyTo(imgStream);
                }
            }
        }
    }
}
