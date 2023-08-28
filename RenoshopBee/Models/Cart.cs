using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.CartInterfaces;
using RenoshopBee.Interfaces.ProductInterfaces;

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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductContext _productContext;
        public CartMethod(ApplicationDbContext context,
            IHttpContextAccessor contextAccessor,
            IProductContext productContext)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _productContext = productContext;
        }
        private Cart CreateCart()
        {
            Cart cart = new Cart();
            cart.products = new List<Product>();
            return cart;
        }
        public void AddProductToCart(int productId)
        {
            var product = _productContext.GetProductById(productId);
            var cart = GetCart();
            cart.products.Add(product);
            cart.TotalPrice = cart.products.Sum(p => p.Price);
            cart.Quantity += 1;
            _contextAccessor.HttpContext.Session.SetString("_cart", JsonConvert.SerializeObject(cart));
        }

        public Cart GetCart()
        {
            var cart = CreateCart();
            var cartJson = _contextAccessor.HttpContext.Session.GetString("_cart");
            if (cartJson != null)
            {
                cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            }
            return cart;
        }

        public void RemoveProductFromCart(int productId,Cart cart)
        {
            Product product =cart.products.Where(product=> product.ID == productId).FirstOrDefault();
            var x = cart.products.Contains(product);
            cart.products.Remove(product);
        }
        public int GetCartItemsNum()
        {
           return GetCart().products.Count;
        }

        public bool IsCartEmpty()
        {
            return (GetCartItemsNum() <= 0);
        }
        private void closeCart()
        {
            if(IsCartEmpty())
                _contextAccessor.HttpContext.Session.Clear();
        }


    }
}
