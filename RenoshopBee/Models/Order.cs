using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenoshopBee.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string CustomerId { get; set; }
        [Required , DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required,DataType(DataType.Currency)]
        public decimal? ShippingPrice { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal SubTotalPrice { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
        [Required]
        public int TotalQuantity { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        public ApplicationUser Customer { get; set; }
        public List<OrderItem> orderItems { get; set; }

    }
}
