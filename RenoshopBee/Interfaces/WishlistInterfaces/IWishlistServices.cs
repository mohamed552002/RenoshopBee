using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.WishlistInterfaces
{
    public interface IWishlistServices
    {
        public void AddItemToWishList(int ProductId);
        public bool doesWishlistExist();
        ICollection<Product> GetWishListitemsProducts();
        public void DeleteWishlistItem(int ProductId);
        public bool doesWishListItemExist(int ProductId);
    }
}
