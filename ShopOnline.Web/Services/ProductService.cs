using ShopOnline.Models.DTOs;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiProductURL = "api/Product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> LoadItemsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiProductURL);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return Enumerable.Empty<ProductDto>();
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProductDto> GetItemAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiProductURL}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(ProductDto);
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
