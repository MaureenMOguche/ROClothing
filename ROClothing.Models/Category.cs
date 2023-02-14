using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ROClothing.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string CategoryName { get; set; }

		[ValidateNever]
		public string CategoryImage { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; } = DateTime.Now.Date;


		//Relationships
		[Required]
		public int ProductTypeId { get; set; }
		[ForeignKey("ProductTypeId")]
		public ProductType ProductType { get; set; }
	}
}
