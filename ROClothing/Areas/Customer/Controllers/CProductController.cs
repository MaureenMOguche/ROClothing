using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;
using ROClothing.Models.ViewModels;
using System.Security.Claims;

namespace ROClothing.Areas.Admin.Controllers
{
	[Area("Customer")]
	public class CProductController : Controller
	{
		[BindProperty]
		public ShoppingCart cartItems { get; set; }
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

		public IActionResult Details(int ProductId)
		{
			cartItems = new()
			{
				NoOfItems = 1,
				ProductId = ProductId,
				Product = _dbContext.ProductRepo.FindFirst(x => x.Id == ProductId, includeProperties: "Category"),
			};
			return View(cartItems);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			shoppingCart.ApplicationUserId = claim.Value;

			var cartFromDb = _dbContext.ShoppingCartRepo.FindFirst(x => x.ProductId == shoppingCart.ProductId);

			if (cartFromDb == null)
			{
				_dbContext.ShoppingCartRepo.Add(shoppingCart);
			}
			else
			{
				_dbContext.ShoppingCartRepo.IncrementShoppingCart(cartFromDb, shoppingCart.NoOfItems);
			}
			_dbContext.Save();



			return RedirectToAction(nameof(Index));
		}
	}
}
