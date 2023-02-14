using ROClothing.Models;

namespace ROClothing.Data.Repository.IRepository
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        void Update(ProductType productType);
    }
}
