using RenoshopBee.Models;

namespace RenoshopBee.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<Product> products { get; set; }

    }
}
