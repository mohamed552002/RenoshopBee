using RenoshopBee.Interfaces.OrderItemInterfaces;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.OrderItemImp
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IProductContext _productContext;
        public OrderItemServices(IProductContext productContext)
        {
            _productContext = productContext;
        }

        public void assignProductForEachItem(List<OrderItem> items)=>
            items.ForEach(item => item.TotalPrice = _productContext.GetProductById(item.ProductId).Price * item.Quantity);

        public void calcTotalPriceForEachItem(List<OrderItem> items)=>
                items.ForEach(item => item.product = _productContext.GetProductById(item.ProductId));
    }
}
