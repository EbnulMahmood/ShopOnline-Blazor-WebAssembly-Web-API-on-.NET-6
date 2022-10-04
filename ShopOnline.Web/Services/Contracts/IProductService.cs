using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> LoadItems();
        Task<ProductDto> GetItem(int id);
    }
}
