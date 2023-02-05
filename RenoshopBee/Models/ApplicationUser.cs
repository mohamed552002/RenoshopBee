using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RenoshopBee.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required,MaxLength(50),MinLength(2)]
        public string FirstName { get; set; }
        [Required,MaxLength(50),MinLength(2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public char Gender { get; set; }
        public bool EmailReceiveable { get; set; }
        public Address address { get; set; }
        [ValidateNever]
        public string Img_Url { get; set; }
    }
}
