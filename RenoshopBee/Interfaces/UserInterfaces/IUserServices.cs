using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.UserInterfaces
{
    public interface IUserServices
    {
        public Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        public Task<ApplicationUser> GetCurrenUserDetailsAsync();
        public Task<string> GetuserId();
    }
}
