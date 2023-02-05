using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RenoshopBee.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required,MaxLength(50),MinLength(2)]
        public string Country { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string City { get; set; }
        [Required, MaxLength(256), MinLength(5)]
        public string Street { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }



    }
}
