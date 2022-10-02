using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _context;

        public ProductRepository(ShopOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> LoadProductsAsync()
        {
            IEnumerable<Product> products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<IEnumerable<ProductCategory>> LoadCategoriesAsync()
        {
            IEnumerable<ProductCategory> productCategories = await _context.ProductCategories.ToListAsync();
            return productCategories;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
