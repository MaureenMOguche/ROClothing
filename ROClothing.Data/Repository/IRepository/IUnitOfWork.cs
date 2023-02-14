namespace ROClothing.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepo { get; }
        public IProductRepository ProductRepo { get; }
        public IProductTypeRepository ProductTypeRepo { get; }


        void Save();
    }
}
