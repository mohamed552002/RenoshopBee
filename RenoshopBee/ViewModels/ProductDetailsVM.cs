using RenoshopBee.Models;

namespace RenoshopBee.ViewModels
{

    public class ProductDetailsVM:BaseViewModel
    {
        public ProductDetailsVM(Product product, IEnumerable<UsersReviews>? usersReviews, IEnumerable<Product> products)
        {
            this.product = product;
            this.usersReviews = usersReviews;
            this.products = products;
        }

        public Product product { get; set; }
        public IEnumerable<UsersReviews>? usersReviews { get; set; }
        public IEnumerable<Product> products { get; set; }
        public ProductReview review { get; set; }
    }
    public class UsersReviews
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Img_Url { get; set; }
        public string ReviewBody { get; set; }
        
    }
}
