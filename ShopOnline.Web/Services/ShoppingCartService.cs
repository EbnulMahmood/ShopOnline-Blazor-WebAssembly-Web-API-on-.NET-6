using Newtonsoft.Json;
using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;

namespace ShopOnline.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiShoppingCartURL = "api/ShoppingCart";

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiShoppingCartURL, cartItemToAddDto);
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

        public async Task<CartItemDto> DeleteItemAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiShoppingCartURL}/{id}");
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

        public async Task<ICollection<CartItemDto>> LoadItemsAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiShoppingCartURL}/{userId}/LoadItems");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return Enumerable.Empty<CartItemDto>().ToList();

                    return await response.Content.ReadFromJsonAsync<ICollection<CartItemDto>>();
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message: {errorMessage}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> UpdateQtyAsync(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
                var response = await _httpClient.PatchAsync($"{_apiShoppingCartURL}/{cartItemQtyUpdateDto.CartItemId}", content);
                if (response.IsSuccessStatusCode)
                {
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
    }
}
