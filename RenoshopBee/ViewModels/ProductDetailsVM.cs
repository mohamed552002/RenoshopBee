using RenoshopBee.Models;
using System.ComponentModel.DataAnnotations;

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
        public int? ProductToEditID { get; set; }
        public double totalRate { get; set; }
    }
    public class UsersReviews
    {
        public int ReviewId { get; set; }
        public string UserId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Img_Url { get; set; }
        public string ReviewBody { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastUpdatedAt { get; set; }
        public int Rate { get; set; }


    }
}
