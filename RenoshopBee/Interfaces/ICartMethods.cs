using RenoshopBee.Models;

namespace RenoshopBee.Interfaces
{
    public interface ICartMethods
    {
        public void AddProductToCart(int productId);
        public void RemoveProductFromCart(int productId, Cart cart);
        public Cart GetCart(int productId);
        public int GetCartItemsNum(Cart cart);
    }
}
