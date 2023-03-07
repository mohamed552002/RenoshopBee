using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenoshopBee.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        [Required,MaxLength(12),MinLength(12)]

        public string CardNumber { get; set; }
        [Required, MaxLength(3), MinLength(3)]
        public string CCV { get; set; }
        [DataType(DataType.Date),Required]
        public DateTime ExpireDate { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser user { get; set; }
    }
}
