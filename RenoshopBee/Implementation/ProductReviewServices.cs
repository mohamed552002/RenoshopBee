using Microsoft.EntityFrameworkCore;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Models;
using RenoshopBee.ViewModels;

namespace RenoshopBee.Implementation
{
    public class ProductReviewServices : IProductReview
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserContext _userContext;
        public ProductReviewServices(ApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IEnumerable<UsersReviews> JoinUserWithReview(IEnumerable<ApplicationUser> user,IEnumerable<ProductReview> productReview)
        {
            return user
                .Join(productReview, user => user.Id
                , product => product.UserId
                , (user, productReview) => new UsersReviews
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Img_Url = user.Img_Url,
                    ReviewBody = productReview.ReviewBody
                });
        }

        public async Task<IEnumerable<UsersReviews>> ViewProductReviewsAsync(int productId)
        {
            var productReview = await _context.ProductReviews.Where(p => p.ProductId == productId).ToListAsync();
            if (productReview.Count > 0)
            {
                var user = await _userContext.GetUsersAsync();
                return JoinUserWithReview(user, productReview);
            }
            else
            {
                return null;
            }
        }
    }
}
