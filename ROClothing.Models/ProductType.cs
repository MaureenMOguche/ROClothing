using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ROClothing.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        public string? ProductTypeImage { get; set; }

        [Required]
        public string ProductTypeName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;

    }
}
