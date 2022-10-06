using ShopOnline.Models.DTOs;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<ICollection<CartItemDto>> LoadItemsAsync(int userId);
        Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItemAsync(int id);
        Task<CartItemDto> UpdateQtyAsync(CartItemQtyUpdateDto cartItemQtyUpdateDto);
    }
}
