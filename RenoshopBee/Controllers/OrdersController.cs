using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RenoshopBee.Data;
using RenoshopBee.Models;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrdersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var cart = GetCartItems();
            if (cart != null)
            {
                foreach(var product in cart.products) {
                    product.availableSizes = _context.productSizes.Where(p => p.ProductId == product.ID).ToList();
                }
                return View("cart", cart);
            }
            else
                return View("cart");
        }
        public Cart GetCartItems()
        {
            Cart cart = new Cart();
            var cartJson = HttpContext.Session.GetString("_cart");
            if (cartJson != null)
            {
                cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                return cart;
            }
            return null;
        }
        public IActionResult Checkout(List<OrderItem> items) {

            if (ModelState.IsValid)
            {
                int totalQuantity = 0;
                decimal subPrice = 0;
                foreach(var item in items)
                {
                    Product product = _context.Products.Find(item.ProductId);
                    item.TotalPrice = product.Price*item.Quantity;
                    item.product = product;
                    totalQuantity += item.Quantity;
                    subPrice += item.TotalPrice;
                }
                var order = new Order()
                {
                    CustomerId = _userManager.GetUserId(User),
                    OrderDate = DateTime.Now,
                    ShippingPrice = 0,
                    SubTotalPrice = subPrice,
                    TotalPrice = subPrice,
                    TotalQuantity = totalQuantity,
                    PaymentMethod = "Cash",
                    orderItems = items
                };
                HttpContext.Session.SetString("Order",JsonConvert.SerializeObject(order)) ;
                CheckoutViewModel checkoutView = new CheckoutViewModel
                {
                    order = order
                };
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
