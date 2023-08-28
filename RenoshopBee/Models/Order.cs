using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenoshopBee.Models
{
    public class Order
    {
        public Order(string CustomerId,decimal SubTotalPrice,int TotalQuantity)
        {
            this.CustomerId = CustomerId;
            this.OrderDate=DateTime.Now;
            this.ShippingPrice=0;
            this.SubTotalPrice = SubTotalPrice;
            this.TotalPrice= SubTotalPrice;
            this.TotalQuantity= TotalQuantity;
            this.PaymentMethod = "Cash";
        }
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
        [ValidateNever,NotMapped]
        public ApplicationUser Customer { get; set; }
        [ValidateNever]
        public List<OrderItem> orderItems { get; set; }

    }
}
