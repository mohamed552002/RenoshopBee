namespace RenoshopBee.Models
{
    public class Wishlist
    {
        public Wishlist(string UserId)
        {
            this.UserId = UserId;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<WishlistItem> wishlistItems { get; set; }
        public ApplicationUser User { get; set; }

    }
}
