﻿using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;

namespace ROClothing.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RODbContext _db;
        public UnitOfWork(RODbContext db)
        {
            _db = db;
            CategoryRepo = new CategoryRepository(_db);
            ProductRepo = new ProductRepository(_db);
            ProductTypeRepo = new ProductTypeRepository(_db);
        }

        public IProductRepository ProductRepo { get; private set; }

        public IProductTypeRepository ProductTypeRepo { get; private set; }

        public ICategoryRepository CategoryRepo { get; private set; }



        //Save changes
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
