using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.OrderInterfaces
{
    public interface IOrderServices
    {
        public Task<Order> CreateOrder(List<OrderItem> items);
    }
}
