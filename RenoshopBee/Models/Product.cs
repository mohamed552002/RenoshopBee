using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenoshopBee.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(50,ErrorMessage  = "Product name mustn't exceed 50 letter")]
        [MinLength(3, ErrorMessage = "Product name mustn't be less than 3 letter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        [Range(1,int.MaxValue,ErrorMessage = "product price must be more than 1 dollars")]
        [DataType("int")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product description is required")]
        [MaxLength(500, ErrorMessage = "Product description mustn't exceed 500 letter")]
        [MinLength(20, ErrorMessage = "Product name mustn't be less than 20 letter")]
        public string Description { get; set; }
        [Required(ErrorMessage ="seller name is required")]
        [MaxLength(50, ErrorMessage = "Seller name mustn't exceed 50 letter")]
        [MinLength(3, ErrorMessage = "Seller name mustn't be less than 3 letter")]
        public string Seller_Name { get; set; }

        [Required(ErrorMessage = "The number of products available in stock required")]
        [DisplayName("Available in stock")]
        public int Av_in_stock { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created_at { get; set; }
        public int NumOfSales { get; set; }
        public DateTime Last_updated_at { get; set; }
        [ValidateNever]
        [DisplayName("Image")]
        public string Img_url { get; set; }
        [DisplayName("Active")]
        public bool Is_active { get; set; }
        [ValidateNever]
        [NotMapped]
        public ICollection<ProductReview> productReview { get; set; }

    }
}
