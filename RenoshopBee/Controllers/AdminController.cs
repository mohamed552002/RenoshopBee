using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using RenoshopBee.Data;

namespace RenoshopBee.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        public AdminController(IEmailSender emailSender, ApplicationDbContext context)
        {
            _emailSender = emailSender;
            _context = context;
        }
        public IActionResult Emails()
        {
            return View("SendEmails");
        }
        public IActionResult SendEmailToAllUsers(string subject,string message)
        {
            foreach (var user in _context.Users)
            {
                _emailSender.SendEmailAsync(user.Email,subject, message);
            }
            return RedirectToAction("Emails");
        }
    }
}
