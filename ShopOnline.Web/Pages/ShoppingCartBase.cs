using Microsoft.AspNetCore.Components;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; private set; }
        public ICollection<CartItemDto> CartItems { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShoppingCartService.LoadItemsAsync(HardCoded.UserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private CartItemDto GetCartItem(int id)
        {
            return CartItems.FirstOrDefault(c => c.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            CartItems.Remove(cartItemDto);
        }

        protected async Task DeleteCartItemClick(int id)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.DeleteItemAsync(id);
                RemoveCartItem(cartItemDto.Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
