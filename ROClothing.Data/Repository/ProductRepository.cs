using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;

namespace ROClothing.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly RODbContext _db;
        public ProductRepository(RODbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.ProductName = product.ProductName;
                productFromDb.ProductDescription = product.ProductDescription;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price = product.Price;
                productFromDb.CategoryId = product.CategoryId;
                if (product.ProductImage != null)
                {
                    productFromDb.ProductImage = product.ProductImage;
                }
            }
        }
    }
}
