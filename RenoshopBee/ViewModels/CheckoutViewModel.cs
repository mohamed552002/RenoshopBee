using RenoshopBee.Models;

namespace RenoshopBee.ViewModels
{
    public class CheckoutViewModel
    {
        public Order order { get; set; }
        public CreditCard credit { get; set; }
    }
}
