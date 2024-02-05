using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.CartInterfaces;
using RenoshopBee.Interfaces.OrderInterfaces;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderServices _orderServices;
        private readonly ICartMethods _cartMethods;
        private readonly IProductSizes _productSizes;
        public OrdersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IOrderServices orderServices,
            ICartMethods cartMethods,
            IProductSizes productSizes)
        {
            _context = context;
            _userManager = userManager;
            _orderServices = orderServices;
            _cartMethods = cartMethods;
            _productSizes = productSizes;
        }
        public async Task<IActionResult> Index()
        {
            var cart = _cartMethods.GetCart();
            if (cart != null) 
            {
                cart = _productSizes.GetAllCartProductSizes(cart);
                return View("cart", cart);
            }
            else
                return View("cart");
        }
        public async Task<IActionResult> Checkout(List<OrderItem> items) {
            
            if (ModelState.IsValid)
            {
                Order order = _orderServices.CreateOrder(items);
                CheckoutViewModel checkoutView = new CheckoutViewModel(order){};
                return View("checkout", checkoutView);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> OrderComplete(CreditCard credit)
        {
            var userId = _userManager.GetUserId(User);
            credit.UserId = userId;
            if (ModelState.IsValid)
            {

                Order order = HttpContext.Session.GetString("Order")!=null?  JsonConvert.DeserializeObject<Order>( HttpContext.Session.GetString("Order")):null ;
                if (order != null && credit !=null)
                {
                    _context.orders.Add(order);
                    _context.orderItems.AddRange(order.orderItems);
                    _context.Add(credit);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.Clear();
                }
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
    }
}
