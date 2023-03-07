using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RenoshopBee.Data;
using RenoshopBee.Models;
using RenoshopBee.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace RenoshopBee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            IWebHostEnvironment webHostEnvironment,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Cart cart = GetCartItems();

            HomeViewModel homeView = new HomeViewModel()
            {
                products = await _context.Products.OrderByDescending(p => p.Created_at).Take(9).ToListAsync(),
                cart=cart
            };
            return View(homeView);
        }
        public async Task<IActionResult> UserDetails()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Address address = await _context.Address.Where(ad => ad.ApplicationUserId == user.Id).FirstOrDefaultAsync();
            user.address = address;

            ViewBag.Age = DateTime.Now.Year - user.BirthDate.Year;
            return View(user);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            Cart cart = new Cart();
             var product = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
            if (HttpContext.Session.GetString("_cart") == null)
            {
                cart.products = new List<Product>
                {
                    product
                };
                cart.TotalPrice = cart.products.Sum(p => p.Price);
                cart.Quantity = 1;
                HttpContext.Session.SetString("_cart", JsonConvert.SerializeObject(cart));
            }
            else
            {

                var cartJson = HttpContext.Session.GetString("_cart");
                cart = JsonConvert.DeserializeObject<Cart>(cartJson);
                if (!(cart.products.Any(product=>product.ID == id)))
                {
                    cart.products.Add(product);
                    cart.TotalPrice = cart.products.Sum(p => p.Price);
                    cart.Quantity += 1;
                    HttpContext.Session.SetString("_cart", JsonConvert.SerializeObject(cart));
                }
                else
                    ViewData["InCart"] = "Product Already in cart";
            }

            return Redirect(returnUrl);
        }
        public async Task<IActionResult> Cart()
        {
            return View("cart");
        }
        public Cart GetCartItems()
        {
            Cart cart = new Cart();
            var cartJson = HttpContext.Session.GetString("_cart");
            Console.WriteLine(cartJson);
            if(cartJson!=null)
                cart = JsonConvert.DeserializeObject<Cart>(cartJson) ;
            return cart;
        }

            [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}