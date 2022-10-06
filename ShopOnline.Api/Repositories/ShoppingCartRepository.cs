using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext _context;

        public ShoppingCartRepository(ShopOnlineDbContext context)
        {
            _context = context;
        }

        private async Task<bool> IsCartItemExists(int cartId, int productId)
        {
            return await _context.CartItems.AnyAsync(c => c.CartId == cartId
                && c.ProductId == productId);
        }

        public async Task<CartItem> AddCartItemAsync(CartItemToAddDto cartItemToAddDto)
        {
            if (await IsCartItemExists(cartItemToAddDto.CartId,
                cartItemToAddDto.ProductId)) return null;
            
            var cartItem = await (from product in _context.Products
                where product.Id == cartItemToAddDto.ProductId
                select new CartItem
                {
                    CartId = cartItemToAddDto.CartId,
                    ProductId = product.Id,
                    Qty = cartItemToAddDto.Qty,
                }).SingleOrDefaultAsync();
            
            if (cartItem == null) return null;
            var result = await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public Task<CartItem> UpdateQtyAsync(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
        
        public Task<CartItem> DeleteCartItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetCartItemAsync(int id)
        {
            return await (from cart in _context.Carts
                join cartItem in _context.CartItems
                on cart.Id equals cartItem.CartId
                where cartItem.Id == id
                select new CartItem
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    Qty = cartItem.Qty,
                    CartId = cartItem.CartId,
                }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> LoadCartItemsAsync(int userId)
        {
            return await (from cart in _context.Carts
                join cartItem in _context.CartItems
                on cart.Id equals cartItem.CartId
                where cart.UserId == userId
                select new CartItem
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    Qty = cartItem.Qty,
                    CartId = cartItem.CartId
                }).ToListAsync();
        }
    }
}