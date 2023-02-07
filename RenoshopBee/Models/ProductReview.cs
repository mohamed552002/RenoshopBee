using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenoshopBee.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        [Required,StringLength(256,MinimumLength = 4)]
        public string ReviewBody { get; set; }
        [ForeignKey("User")]
        [ValidateNever]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ValidateNever]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product product { get; set; }
    }
}
