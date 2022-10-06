using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ShoppingCart", cartItemToAddDto);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(CartItemDto);

                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message: {errorMessage}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CartItemDto>> LoadItemsAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/LoadItems");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return Enumerable.Empty<CartItemDto>();

                    return await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message: {errorMessage}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
