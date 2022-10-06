using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> LoadItemsAsync();
        Task<ProductDto> GetItemAsync(int id);
    }
}
