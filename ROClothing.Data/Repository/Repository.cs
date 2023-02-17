using Microsoft.EntityFrameworkCore;
using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;
using System.Linq.Expressions;

namespace ROClothing.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RODbContext _db;
        public DbSet<T> dbSet;

        public Repository(RODbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T FindFirst(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(predicate);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(property);

                }

            }
            return query.ToList();
        }

        // Include Properties
        //public IEnumerable<T> GetAll(string[]? includeProperties = null)
        //{
        //    IQueryable<T> query = dbSet;
        //    if (includeProperties != null)
        //    {
        //        foreach (var property in includeProperties)
        //        {
        //            if (property.Contains(','))
        //            {
        //                string[] items = property.Split(',');
        //                query = query.Include(items[0]).ThenInclude(items[1]);
        //            }

        //            query = query.Include(property);

        //        }

        //    }
        //    return query.ToList();
        //}

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
