namespace ROClothing.Models.ViewModels
{
	public class ProductProductTypeVM
	{
		public IEnumerable<ProductType> productTypeList { get; set; }
		public IEnumerable<Product> productList { get; set; }
		public decimal StartingPrice { get; set; }
	}
}
