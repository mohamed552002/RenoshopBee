using Microsoft.CodeAnalysis;
using RenoshopBee.Data;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Interfaces.WishlistInterfaces;
using RenoshopBee.Models;

namespace RenoshopBee.Implementation.WishlistImp
{
    public class WishlistServices : IWishlistServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserServices _userContext;
        private readonly IProductContext _productContext;
        private readonly IProductSizes _productSizes;
        public WishlistServices(ApplicationDbContext context, IProductSizes productSizes,
            IUserServices userContext, IProductContext productContext)
        {
            _context = context;
            _userContext = userContext;
            _productContext = productContext;
            _productSizes = productSizes;
        }
        private Wishlist GetWishList()
        {
           return  _context.wishlists.FirstOrDefault(w => w.UserId == _userContext.GetuserId());
        }
        private void AddFirstTimeWishList()
        {
            _context.wishlists.Add(new Wishlist(_userContext.GetuserId()));
            _context.SaveChanges();
        }
        private void AddWishlistItem(int ProductId)
        {
            _context.wishlistItem.Add(new WishlistItem(ProductId,GetWishList().Id));
            _context.SaveChanges();
        }
        public void AddItemToWishList(int ProductId)
        {
            if (!doesWishlistExist())
            {
                AddFirstTimeWishList();
            }
            AddWishlistItem(ProductId);      
        }
        private ICollection<WishlistItem> GetWishListItems()=> _context.wishlistItem.Where(item => item.WishlistId == GetWishList().Id).ToList();
        private List<Product> ExtractProductsAndProductSizes(ICollection<WishlistItem> wishListItems)
        {
            List<Product> wishlistProducts = new List<Product>();
            foreach (var item in wishListItems)
            {
                var Product = _productContext.GetProductById(item.ProductId);
                wishlistProducts.Add(Product);
                wishlistProducts.ForEach(product => product.availableSizes = _productSizes.GetProductSizes(Product));
            }
            return wishlistProducts;
        }
        public ICollection<Product> GetWishListitemsProducts()        
        {
            var wishListItems = GetWishListItems();
            return ExtractProductsAndProductSizes(wishListItems);
        }

        public bool doesWishlistExist()
        {
            return GetWishList()!= null;
        }

        public void DeleteWishlistItem(int ProductId)
        {
            WishlistItem item = _context.wishlistItem.FirstOrDefault(item => item.ProductId == ProductId);
            _context.wishlistItem.Remove(item);
            _context.SaveChanges();
        }

        public bool doesWishListItemExist(int ProductId)
        {
            return _context.wishlistItem.Any(item => item.ProductId == ProductId);
        }
    }
}
