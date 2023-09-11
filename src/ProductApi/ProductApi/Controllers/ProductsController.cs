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

        [HttpPost]
        public async Task<ActionResult<CreateProductResponseDto>> AddProduct(CreateProductRequestDto product)
        {
            var response = await _productService.CreateProduct(product);

            return Ok(response);
        }
    }
}
