namespace ROClothing.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepo { get; }
        public IProductRepository ProductRepo { get; }
        public IProductTypeRepository ProductTypeRepo { get; }
        public IApplicationUserRepository ApplicationUserRepo { get; }
        public IShoppingCartRepository ShoppingCartRepo { get; }


        void Save();
    }
}
