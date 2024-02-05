using RenoshopBee.Models;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductReview
    {
        public IEnumerable<UsersReviews> JoinUserWithReview(IEnumerable<ApplicationUser> user, IEnumerable<ProductReview> review);
        public Task<IEnumerable<UsersReviews>> ViewProductReviewsAsync(int productId);
        public ProductReview GetProductReviewById(int productId);
    }
}
