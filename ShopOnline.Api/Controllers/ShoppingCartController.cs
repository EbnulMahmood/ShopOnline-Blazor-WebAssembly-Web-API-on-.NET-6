using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartRepository cartRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("{userId}/LoadItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> LoadItemsAsync(int userId)
        {
            try
            {
                var cartItems = await _cartRepository.LoadCartItemsAsync(userId);
                if (cartItems == null) return NoContent();

                var products = await _productRepository.LoadProductsAsync();
                if (products == null) throw new Exception("No products exist in the system.");

                var cartItemsDto = cartItems.ConvertToDto(products);
                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<CartItemDto>> GetItemAsync(int id)
        {
            try
            {
                var cartItem = await _cartRepository.GetCartItemAsync(id);
                if (cartItem == null) return NotFound();

                var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product == null) return NotFound();

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddItemAsync([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await _cartRepository.AddCartItemAsync(cartItemToAddDto);
                if (newCartItem == null) return NoContent();

                var product = await _productRepository.GetProductByIdAsync(newCartItem.ProductId);
                if (product == null) throw new Exception($"Something went wrong when attempting" +
                    $" to retrieve product (productId:{newCartItem.ProductId})");
                
                var newCartItemDto = newCartItem.ConvertToDto(product);
                return CreatedAtAction(nameof(GetItemAsync), new {id = newCartItemDto.Id}, newCartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItemAsync(int id)
        {
            try
            {
                var cartItem = await _cartRepository.DeleteCartItemAsync(id);
                if (cartItem == null) return NotFound();

                var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product == null) return NotFound();

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}