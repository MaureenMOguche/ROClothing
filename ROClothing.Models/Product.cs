using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ROClothing.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public DateTime? CreatedDate { get; set; } = DateTime.Now.Date;
        [ValidateNever]
        public string? ProductImage { get; set; }
        [Required]
        public double ListPrice { get; set; }
        [Required]
        public double Price { get; set; }
        public double? Price50 { get; set; }


        //Relationships
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        //[Required]
        //public int ProductTypeId { get; set; }
        //[ForeignKey("ProductTypeId")]
        //[ValidateNever]
        //public ProductType ProductType { get; set; }
    }
}
