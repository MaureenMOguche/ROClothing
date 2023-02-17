namespace ROClothing.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> cartList { get; set; }
        public double cartTotal { get; set; }
    }
}
