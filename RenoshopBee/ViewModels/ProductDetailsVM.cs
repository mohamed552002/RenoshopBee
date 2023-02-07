using RenoshopBee.Models;

namespace RenoshopBee.ViewModels
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public IEnumerable<UsersReviews>? usersReviews { get; set; }
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
