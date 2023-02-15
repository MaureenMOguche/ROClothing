using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;
using ROClothing.Models.ViewModels;

namespace ROClothing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(IUnitOfWork dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _dbContext.CategoryRepo.GetAll(includeProperties: "ProductType");
            return View(categoryList);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            CategoryVM categoryVM = new()
            {
                category = new(),
                producTypetList = _dbContext.ProductTypeRepo.GetAll()
                                                .Select(x => new SelectListItem
                                                {
                                                    Text = x.ProductTypeName,
                                                    Value = x.Id.ToString(),
                                                })
            };

            if (id == null || id == 0)
            {
                //create category

                return View(categoryVM);
            }
            else
            {
                //edit item
                categoryVM.category = _dbContext.CategoryRepo.FindFirst(x => x.Id == id);
                return View(categoryVM);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CategoryVM categoryVM, IFormFile? categoryImg)
        {

            if (ModelState.IsValid)
            {
                var webrootpath = _hostEnvironment.WebRootPath;

                if (categoryImg != null)
                {
                    var uploads = Path.Combine(webrootpath, @"images\categoryImages");
                    var filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(categoryImg.FileName);

                    //Delete old image
                    if (categoryVM.category.CategoryImage != null)
                    {
                        var OldImgPath = Path.Combine(webrootpath, categoryVM.category.CategoryImage.TrimStart('\\'));

                        if (System.IO.File.Exists(OldImgPath))
                        {
                            System.IO.File.Delete(OldImgPath);
                        }
                    }

                    //Add new Image
                    using (var filestream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        categoryImg.CopyTo(filestream);
                    }
                    categoryVM.category.CategoryImage = @"\images\categoryImages\" + filename + extension;
                }


                if (categoryVM.category.Id == 0)
                {
                    _dbContext.CategoryRepo.Add(categoryVM.category);
                }
                else
                {
                    _dbContext.CategoryRepo.Update(categoryVM.category);
                }

                _dbContext.Save();
                TempData["Success"] = "Successfully created new category";
                return RedirectToAction("Index");
            }
            else
            {
                return View(categoryVM);
            }
        }


        public IActionResult Delete(int id)
        {
            var category = _dbContext.CategoryRepo.FindFirst(x => x.Id == id);

            //Delete old image
            var webrootpath = _hostEnvironment.WebRootPath;

            var OldImgPath = Path.Combine(webrootpath, category.CategoryImage.TrimStart('\\'));

            if (System.IO.File.Exists(OldImgPath))
            {
                System.IO.File.Delete(OldImgPath);
            }

            //Remove category item from Db    
            _dbContext.CategoryRepo.Remove(category);
            _dbContext.Save();
            return RedirectToAction("Index");
        }
    }
}