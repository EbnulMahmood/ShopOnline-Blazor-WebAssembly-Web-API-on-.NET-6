using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopOnline.Api.Entities;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddCartItemAsync(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQtyAsync(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteCartItemAsync(int id);
        Task<CartItem> GetCartItemAsync(int id);
        Task<IEnumerable<CartItem>> LoadCartItemsAsync(int userId);
    }
}