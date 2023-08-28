using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.CartInterfaces
{
    public interface ICartMethods
    {
        public void AddProductToCart(int productId);
        public void RemoveProductFromCart(int productId, Cart cart);
        public Cart GetCart();
        public int GetCartItemsNum();
        public bool IsCartEmpty();
    }
}
