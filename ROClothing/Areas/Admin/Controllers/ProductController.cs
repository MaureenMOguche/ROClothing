using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;
using ROClothing.Models.ViewModels;

namespace ROClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _dbContext;
		public readonly IWebHostEnvironment _hostEnvironment;
		public ProductController(IUnitOfWork dbContext, IWebHostEnvironment hostEnvironment)
		{
			_dbContext = dbContext;
			_hostEnvironment = hostEnvironment;
		}
		public IActionResult Index()
		{
			IEnumerable<Product> productList = _dbContext.ProductRepo.GetAll(includeProperties: "Category");
			return View(productList);
		}
		public IActionResult Upsert(int? id)
		{
			//Creates View model that binds Product with Category and Product type
			ProductVM productVM = new()
			{
				Product = new(),
				categories = _dbContext.CategoryRepo.GetAll().Select(c => new SelectListItem
				{
					Text = c.CategoryName,
					Value = c.Id.ToString(),
				}),

				productTypes = _dbContext.ProductTypeRepo.GetAll().Select(c => new SelectListItem
				{
					Text = c.ProductTypeName,
					Value = c.Id.ToString(),
				}),
			};

			if (id == null || id == 0)
			{
				return View(productVM);
				//return PartialView("_UpsertPartial", new Product());

			}
			else
			{
				productVM.Product = _dbContext.ProductRepo.FindFirst(c => c.Id == id);
				return View(productVM);
				//return PartialView("_UpsertPartial", productVM.Product);

			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM productVM, IFormFile? productImg)
		{
			if (ModelState.IsValid)
			{
				var webrootpath = _hostEnvironment.WebRootPath;

				if (productImg != null)
				{
					var uploads = Path.Combine(webrootpath, @"images\productImages");
					var filename = Guid.NewGuid().ToString();
					var extension = Path.GetExtension(productImg.FileName);

					//delete old image
					if (productVM.Product.ProductImage != null)
					{
						var oldImage = Path.Combine(webrootpath, productVM.Product.ProductImage.TrimStart('\\'));
						if (System.IO.File.Exists(oldImage))
						{
							System.IO.File.Delete(oldImage);
						}
					}

					//add new image
					using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
					{
						productImg.CopyTo(filestreams);
					}
					productVM.Product.ProductImage = @"\images\productImages\" + filename + extension;
				}

				if (productVM.Product.Id == 0)
				{
					_dbContext.ProductRepo.Add(productVM.Product);
				}
				else
				{
					_dbContext.ProductRepo.Update(productVM.Product);
				}
				_dbContext.Save();
				TempData["Success"] = "Successfully created new product";
			}


			return RedirectToAction("Index");
		}



		public IActionResult Delete(int id)
		{
			ProductVM productVm = new();
			productVm.Product = _dbContext.ProductRepo.FindFirst(x => x.Id == id);
			return View(productVm);
		}

		[HttpPost]
		public IActionResult Delete(ProductVM productVM, IFormFile? productImg)
		{
			if (ModelState.IsValid)
			{
				var webrootpath = _hostEnvironment.WebRootPath;

		  
					//delete image

						var oldImage = Path.Combine(webrootpath, productVM.Product.ProductImage.TrimStart('\\'));
						if (System.IO.File.Exists(oldImage))
						{
							System.IO.File.Delete(oldImage);
						}
 
					

			   
				_dbContext.ProductRepo.Remove(productVM.Product);
				_dbContext.Save();
				TempData["Success"] = "Successfully created new product";
			}


			return RedirectToAction("Index");
		}
	}
}
