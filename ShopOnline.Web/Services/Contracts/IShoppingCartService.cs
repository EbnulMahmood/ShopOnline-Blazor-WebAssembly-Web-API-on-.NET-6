using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> LoadItemsAsync(int userId);
        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);
    }
}
