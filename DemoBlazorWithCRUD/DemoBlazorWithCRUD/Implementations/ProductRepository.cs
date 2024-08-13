using DemoBlazorWithCRUD.Data;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using SharedLibrary.ProductRepositories;

namespace DemoBlazorWithCRUD.Implementations
{
	public class ProductRepository : IProductReposistory
	{
		private readonly AppDbContext appDbContext;

		public ProductRepository(AppDbContext appDbContext)
        {
			this.appDbContext = appDbContext;
		}
        public async Task<Product> AddProductAsync(Product model)
		{
			if (model == null) return null!;

			var chk = await appDbContext.Products.Where(_=>_.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefaultAsync();
			if (chk is not null) return null!;

			var newDataAdded = appDbContext.Products.Add(model).Entity;
			await appDbContext.SaveChangesAsync();
			return newDataAdded;
		}

		public async Task<Product> DeleteProductAsync(int productId)
		{
			var product = await appDbContext.Products.FirstOrDefaultAsync(_=>_.Id.Equals(productId));
            if (product is null) return null!;

			appDbContext.Products.Remove(product);
			await appDbContext.SaveChangesAsync();
			return product;
        }

		public async Task<List<Product>> GetAllProductsAsync() => await appDbContext.Products.ToListAsync();

		public async Task<Product> GetProductByIdAsync(int productId)
		{
			var product = await appDbContext.Products.FirstOrDefaultAsync(_ => _.Id.Equals(productId));
			if (product is null) return null!;

			return product;
		}

		public async Task<Product> UpdateProductAsync(Product model)
		{
			var product = await appDbContext.Products.FirstOrDefaultAsync(_ => _.Id.Equals(model.Id));
			if (product is null) return null!;

			product.Name = model.Name;
			product.Quantity = model.Quantity;
			appDbContext.Products.Update(product);
			await appDbContext.SaveChangesAsync();
			return product;
		}
	}
}
