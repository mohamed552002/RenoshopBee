using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RenoshopBee.Interfaces.CartInterfaces;
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            _cartMethods.AddProductToCart(id);
            return Redirect(returnUrl);
        }
        public IActionResult DeletefromCart(int id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var cart = _cartMethods.GetCart();
            _cartMethods.RemoveProductFromCart(id,cart);
            if (_cartMethods.GetCartItemsNum() <= 0)
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
