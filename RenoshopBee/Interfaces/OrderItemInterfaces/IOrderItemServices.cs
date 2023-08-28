using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.OrderItemInterfaces
{
    public interface IOrderItemServices
    {
        public void calcTotalPriceForEachItem(List<OrderItem> items);
        public void assignProductForEachItem(List<OrderItem> items);

    }
}
