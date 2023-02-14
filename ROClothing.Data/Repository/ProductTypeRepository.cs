using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;

namespace ROClothing.Data.Repository
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        private readonly RODbContext _db;
        public ProductTypeRepository(RODbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ProductType productType)
        {
            var productTypeFromDb = _db.ProductTypes.FirstOrDefault(p => p.Id == productType.Id);
            if (productTypeFromDb != null)
            {
                productTypeFromDb.ProductTypeName = productType.ProductTypeName;
                if (productType.ProductTypeImage != null)
                {
                    productTypeFromDb.ProductTypeImage = productType.ProductTypeImage;
                }
            }
        }
    }
}
