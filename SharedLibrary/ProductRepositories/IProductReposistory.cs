
using SharedLibrary.Models;

namespace SharedLibrary.ProductRepositories
{
	public interface IProductReposistory
	{
		Task<Product> AddProductAsync(Product model);
		Task<Product> UpdateProductAsync(Product model);
		Task<Product> DeleteProductAsync(int productId);
		Task<List<Product>> GetAllProductsAsync();	
		Task<Product> GetProductByIdAsync(int productId);
	}
}
