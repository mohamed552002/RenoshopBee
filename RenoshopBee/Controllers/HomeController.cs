using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Models;
using System.Diagnostics;
using System.Linq;

namespace RenoshopBee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger,
            IWebHostEnvironment webHostEnvironment,
            ApplicationDbContext context)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.OrderByDescending(p=>p.Created_at).Take(9).ToListAsync());
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