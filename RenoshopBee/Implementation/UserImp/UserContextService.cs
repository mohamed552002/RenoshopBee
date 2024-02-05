using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.UserImp
{
    public class UserContextService : IUserServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetCurrenUserDetailsAsync()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            ApplicationUser user = await _userManager.GetUserAsync(httpContext.User);
            Address address = await _context.Address.Where(ad => ad.ApplicationUserId == user.Id).FirstOrDefaultAsync();
            user.address = address;
            return user;
        }

        public string GetuserId()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            return _userManager.GetUserAsync(httpContext.User).Result.Id;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
