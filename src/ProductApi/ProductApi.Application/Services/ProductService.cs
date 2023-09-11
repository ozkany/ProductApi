using ProductApi.Application.Dtos;
using ProductApi.Domain.Interfaces;
using ProductApi.Domain.Models;

namespace ProductApi.Application.Services
{
    public class ProductService : IProductService
    {
        IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto request)
        {
            var product = await _productRepository.AddAsync(new Product
            {
                Id = request.Id,
                Name= request.Name,
                Description= request.Description,
                Brand= request.Brand,
                Price= request.Price,
                Stock= request.Stock
            });

            await _productRepository.SaveChangesAsync();

            return new CreateProductResponseDto() { Id = product.Id };
        }

        public async Task<GetAllProductsResponseDto> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();

            var resp = new GetAllProductsResponseDto() { Products = new List<Dtos.Base.ProductDto>() };
            return resp;
        }
    }
}
