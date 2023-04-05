using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.User
{
    public class UserContextService : IUserContext
    {
        private readonly ApplicationDbContext _context;
        public UserContextService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
