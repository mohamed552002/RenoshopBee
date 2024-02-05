using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.OrderInterfaces
{
    public interface IOrderServices
    {
        public Order CreateOrder(List<OrderItem> items);
    }
}
