using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View(await _context.Products.OrderByDescending(p=>p.Created_at).Take(9).ToListAsync());
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