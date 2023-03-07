using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenoshopBee.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        [MaxLength(50)]
        public string Size { get; set; }
        [MaxLength(50)]
        public string Color { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
        [ValidateNever,NotMapped]
        public Product product { get; set; }
        [ValidateNever]
        public Order order { get; set; }
    }
}
