namespace RenoshopBee.Models
{
    public class WishlistItem
    {
        public WishlistItem(int ProductId, int WishlistId)
        {
            this.ProductId = ProductId;
            this.WishlistId = WishlistId;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WishlistId { get; set; }
        public Wishlist wishlist { get; set; }
        public Product product { get; set; }
    }
}
