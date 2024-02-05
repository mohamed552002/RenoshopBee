using Microsoft.AspNetCore.Mvc;
using RenoshopBee.Interfaces.WishlistInterfaces;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistServices _wishlistServices;
        public WishlistController(IWishlistServices wishlistServices)
        {
            _wishlistServices = wishlistServices;
        }
        public IActionResult Index()
        {
            var wishListProducts = _wishlistServices.GetWishListitemsProducts();
            var WishlistViewModels = new WishlistViewModels(wishListProducts);
            return View("wishlist", WishlistViewModels);
        }
        public IActionResult AddToWishlist(int productId)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            if (!_wishlistServices.doesWishListItemExist(productId))
            {
                _wishlistServices.AddItemToWishList(productId);
            }
            else
                TempData["ProductAddedAgain"] = true;

            return Redirect(returnUrl);
        }
        [HttpDelete]
        [Route("/DeleteWishlistitem/{productId}")]
        public IActionResult DeleteWishlistitem(int productId)
        {
            _wishlistServices.DeleteWishlistItem(productId);
            return Ok();
        }
        //public IActionResult WishList()
        //{
        //    //_wishlistServices.GetWishListProducts();
        //    return View();
        //}
    }
}
