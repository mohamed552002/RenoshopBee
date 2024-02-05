using RenoshopBee.Models;

namespace RenoshopBee.ViewModels
{
    public class WishlistViewModels
    {
        public WishlistViewModels(ICollection<Product> products)
        {
            this.products = products;
        }
        public ICollection<Product> products { get; set; }
    }
}
