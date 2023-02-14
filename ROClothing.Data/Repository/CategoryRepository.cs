using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;

namespace ROClothing.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly RODbContext _db;
        public CategoryRepository(RODbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (categoryFromDb != null)
            {
                categoryFromDb.CategoryName = category.CategoryName;
                categoryFromDb.ProductTypeId = category.ProductTypeId;

                if (category.CategoryImage != null)
                {
                    categoryFromDb.CategoryImage = category.CategoryImage;
                }
            }
        }
    }
}
