using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ROClothing.Models.ViewModels
{
    public class ProductVM
    {
        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> categories { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> productTypes { get; set; }

    }
}
