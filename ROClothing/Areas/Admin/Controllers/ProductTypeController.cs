using Microsoft.AspNetCore.Mvc;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;

namespace ROClothing.Areas.Admin.Controllers
{
	public class ProductTypeController : Controller
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IWebHostEnvironment _hostEnvironment;

		public ProductTypeController(IUnitOfWork dbContext, IWebHostEnvironment hostEnvironment)
		{
			_dbContext = dbContext;
			_hostEnvironment = hostEnvironment;
		}
		public IActionResult Index()
		{
			IEnumerable<ProductType> productTypeList = _dbContext.ProductTypeRepo.GetAll();
			return View(productTypeList);
		}

		public IActionResult Upsert(int? id)
		{
			if (id == null || id == 0)
			{
				//create product type
				var productTypeItem = new ProductType();
				return View(productTypeItem);
			}
			else
			{
				//edit item
				var productTypeItem = _dbContext.ProductTypeRepo.FindFirst(u => u.Id == id);
				return View(productTypeItem);
			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductType productTypeItem, IFormFile? productTypeImg)
		{
			if (ModelState.IsValid)
			{
				var webrootpath = _hostEnvironment.WebRootPath;

				if (productTypeImg != null)
				{
					var uploads = Path.Combine(webrootpath, @"images\productTypeImages");
					var filename = Guid.NewGuid().ToString();
					var extension = Path.GetExtension(productTypeImg.FileName);


					//delete old Image
					if (productTypeItem.ProductTypeImage != null)
					{
						var oldImagePath = Path.Combine(webrootpath, productTypeItem.ProductTypeImage.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					//Adding new Image
					using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
					{
						productTypeImg.CopyTo(filestreams);
					}

					productTypeItem.ProductTypeImage = @"\images\productTypeImages\" + filename + extension;
				}

				if (productTypeItem.Id == 0)
				{
					_dbContext.ProductTypeRepo.Add(productTypeItem);
				}
				else
				{
					_dbContext.ProductTypeRepo.Update(productTypeItem);
				}

				_dbContext.Save();
				TempData["Success"] = "Successfully created product type";
				return RedirectToAction("Index");
			}
			else
			{
				return View(productTypeItem);
			}
		}

		public IActionResult Delete(int id)
		{
			var webrootpath = _hostEnvironment.WebRootPath;
			var productTypefromDb = _dbContext.ProductTypeRepo.FindFirst(x => x.Id == id);

			var oldImagePath = Path.Combine(webrootpath, productTypefromDb.ProductTypeImage.TrimStart('\\'));

			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			_dbContext.ProductTypeRepo.Remove(productTypefromDb);
			_dbContext.Save();

			return RedirectToAction("Index");
		}
	}
}
