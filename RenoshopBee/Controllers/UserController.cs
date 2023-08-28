using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserServices _userServices;
        public UserController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IUserServices userServices)
        {
            _userManager = userManager;
            _context = context;
            _userServices = userServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserDetails()
        {
            var user = await _userServices.GetCurrenUserDetailsAsync();
            ViewBag.Age = DateTime.Now.Year - user.BirthDate.Year;
            return View(user);
        }
    }
}
