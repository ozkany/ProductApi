using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Base;
using ProductApi.Application.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllProductsResponseDto>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return products;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string productId)
        {
            var product = await _productService.GetProductAsync(productId);

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductResponseDto>> CreateProduct(CreateProductRequestDto product)
        {
            var response = await _productService.CreateProductAsync(product);

            return response;
        }

        [HttpPost(nameof(CreateProductRange))]
        public async Task<IActionResult> CreateProductRange(List<CreateProductRequestDto> products)
        {
            await _productService.CreateProductRangeAsync(products);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestDto product)
        {
            await _productService.UpdateProductAsync(product);

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            await _productService.DeleteProductAsync(productId);

            return Ok();
        }
    }
}
