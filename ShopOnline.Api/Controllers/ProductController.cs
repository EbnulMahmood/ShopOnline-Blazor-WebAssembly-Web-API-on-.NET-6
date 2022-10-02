using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.DTOs;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> LoadItems()
        {
            try
            {
                var products = await _repository.LoadProductsAsync();
                var productCategories = await _repository.LoadCategoriesAsync();
                if (products == null || productCategories == null) return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
