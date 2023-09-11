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
            var products = await _productService.GetAllProducts();

            return products;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string productId)
        {
            var product = await _productService.GetProduct(productId);

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductResponseDto>> CreateProduct(CreateProductRequestDto product)
        {
            var response = await _productService.CreateProduct(product);

            return response;
        }

        [HttpPost(nameof(CreateProductRange))]
        public async Task<IActionResult> CreateProductRange(List<CreateProductRequestDto> products)
        {
            await _productService.CreateProductRange(products);

            return Ok();
        }
    }
}
