using Microsoft.AspNetCore.Mvc.Controllers;
using RenoshopBee.Data;
using RenoshopBee.Interfaces;

namespace RenoshopBee.Models
{
    public class Cart
    {
        public List<Product> products { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }

    }
    public class CartMethod : ICartMethods
    {
        private readonly ApplicationDbContext _context;
        public CartMethod(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddProductToCart(int productId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCart(int productId)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductFromCart(int productId,Cart cart)
        {
            Product product =cart.products.Where(product=> product.ID == productId).FirstOrDefault();
            var x = cart.products.Contains(product);
            cart.products.Remove(product);
        }
        public int GetCartItemsNum(Cart cart)
        {
           return (cart.products.Count);
        }
    }
}
