using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.UserInterfaces
{
    public interface IUserContext
    {
        public Task<IEnumerable<ApplicationUser>> GetUsersAsync();
    }
}
