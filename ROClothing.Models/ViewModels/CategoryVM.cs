using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ROClothing.Models.ViewModels
{
    public class CategoryVM
    {
        [ValidateNever]
        public Category category { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> producTypetList { get; set; }
    }
}
