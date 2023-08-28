using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RenoshopBee.Interfaces.OrderInterfaces;
using RenoshopBee.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using RenoshopBee.Interfaces.UserInterfaces;
using Microsoft.EntityFrameworkCore;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Interfaces.OrderItemInterfaces;
using RenoshopBee.Implementation.OrderItemImp;

namespace RenoshopBee.Implementation.OrderImp
{
    public class OrderServices : IOrderServices
    {
        private readonly IUserServices _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductContext _productContext;
        private readonly IOrderItemServices _orderItemService;

        public OrderServices(IHttpContextAccessor httpContextAccessor, IUserServices userService, IProductContext productContext,IOrderItemServices orderItemService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _productContext = productContext;
            _orderItemService = orderItemService;
        }
        private decimal clacSubOrderPrice(List<OrderItem> items) => (items.Sum(item => item.Quantity* _productContext.GetProductById(item.ProductId).Price));
        private int calcOrderTotalQuantity(List<OrderItem> items) => items.Sum(item => item.Quantity);
        private void handleOrderItems(List<OrderItem> items)
        {
            _orderItemService.calcTotalPriceForEachItem(items);
            _orderItemService.assignProductForEachItem(items);
        }

        public async Task<Order> CreateOrder(List<OrderItem> items)
        {
            handleOrderItems(items);
            var order = new Order(await _userService.GetuserId(), clacSubOrderPrice(items), calcOrderTotalQuantity(items)) { };
            order.orderItems = items;
            _httpContextAccessor.HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(order));
            return order;
        }
    }
}
