using Microsoft.AspNetCore.Mvc;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;
using ROClothing.Models.ViewModels;

namespace ROClothing.Areas.Admin.Controllers
{
    public class CProductController : Controller
    {
        private readonly IUnitOfWork _dbContext;
        public CProductController(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _dbContext.ProductRepo.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cartItems = new()
            {
                NoOfItems = 1,
                Product = _dbContext.ProductRepo.FindFirst(x => x.Id == id, includeProperties: "Category"),
            };
            return View(cartItems);
        }
    }
}
