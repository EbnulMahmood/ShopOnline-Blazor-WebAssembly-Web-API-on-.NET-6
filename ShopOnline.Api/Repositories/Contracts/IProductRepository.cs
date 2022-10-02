using ShopOnline.Api.Entities;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> LoadProductsAsync();
        Task<IEnumerable<ProductCategory>> LoadCategoriesAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<ProductCategory> GetCategoryByIdAsync(int id);
    }
}
