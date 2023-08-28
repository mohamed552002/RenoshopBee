using RenoshopBee.Models;

namespace RenoshopBee.ViewModels
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel(Order order)
        {
            this.order = order;
        }
        public Order order { get; set; }
        public CreditCard credit { get; set; }
    }
}
