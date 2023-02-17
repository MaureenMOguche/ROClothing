using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models;

namespace ROClothing.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly RODbContext _db;
        public ApplicationUserRepository(RODbContext db) : base(db)
        {
            _db = db;
        }
    }
}
