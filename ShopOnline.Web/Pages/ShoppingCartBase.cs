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
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShoppingCartService.LoadItemsAsync(HardCoded.UserId);
                CalculateCartSummaryTotoal();
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

        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if (item == null) return;

            item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
        }

        private void CalculateCartSummaryTotoal()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = CartItems.Sum(p => p.TotalPrice).ToString("C");
        }

        private void SetTotalQuantity()
        {
            TotalQuantity = CartItems.Sum(p => p.Qty);
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
                CalculateCartSummaryTotoal();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateQtyCartItemClick(int id, int qty)
        {
            try
            {
                if(qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty
                    };
                    var returnedUpdateItemDto = await ShoppingCartService.UpdateQtyAsync(updateItemDto);
                    UpdateItemTotalPrice(returnedUpdateItemDto);
                    CalculateCartSummaryTotoal();
                } else
                {
                    var item = CartItems.FirstOrDefault(c => c.Id == id);
                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
