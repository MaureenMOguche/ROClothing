using ROClothing.Data.Data;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models.ViewModels;

namespace ROClothing.Data.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly RODbContext _db;
        public ShoppingCartRepository(RODbContext db) : base(db)
        {
            _db = db;
        }

        public void DecrementShoppingCart(ShoppingCart cart, int Count)
        {
            cart.NoOfItems -= Count;
        }

        public void IncrementShoppingCart(ShoppingCart cart, int Count)
        {
            cart.NoOfItems += Count;
        }

        public double CartTotal(IEnumerable<ShoppingCart> cart)
        {
            double total = 0;
            foreach (var cartItem in cart)
            {
                total += (cartItem.Product.Price * cartItem.NoOfItems);
            }
            return total;
        }
    }
}
