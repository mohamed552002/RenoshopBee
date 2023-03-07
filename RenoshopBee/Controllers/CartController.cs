using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RenoshopBee.Interfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartMethods _cartMethods;
        public CartController(ICartMethods cartMethods)
        {
            _cartMethods = cartMethods;
        }
        public IActionResult DeletefromCart(int id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var cartJson = HttpContext.Session.GetString("_cart");
            var cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            _cartMethods.RemoveProductFromCart(id,cart);
            if (_cartMethods.GetCartItemsNum(cart) <= 0)
            {
                HttpContext.Session.Clear();
            }
            else
            {
                HttpContext.Session.SetString("_cart", JsonConvert.SerializeObject(cart));

            }
            return Redirect(returnUrl);
        }
    }
}
