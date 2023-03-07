using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RenoshopBee.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Country length Mustn't exceed 50 char "), MinLength(2, ErrorMessage = "Country length Mustn't be less than 2 char ")]
        public string Country { get; set; }
        [Required, MaxLength(50, ErrorMessage = "City length Mustn't exceed 50 char "), MinLength(2, ErrorMessage = "City length Mustn't be less than 2 char ")]
        public string City { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Street length Mustn't exceed 50 char "), MinLength(2, ErrorMessage = "Street length Mustn't be less than 2 char ")]
        public string Street { get; set; }
        [ValidateNever]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }



    }
}
