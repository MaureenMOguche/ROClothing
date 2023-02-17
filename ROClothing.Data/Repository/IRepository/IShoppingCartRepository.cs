using ROClothing.Models.ViewModels;

namespace ROClothing.Data.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void IncrementShoppingCart(ShoppingCart cart, int Count);
        void DecrementShoppingCart(ShoppingCart cart, int Count);

        double CartTotal(IEnumerable<ShoppingCart> cart);

    }
}
