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

        public async Task<CreateProductResponse> CreateUser(CreateProductRequest request)
        {
            var user1 = await _productRepository.AddAsync(new Product
            {
                Name= request.Name,
                Description= request.Description,
                Brand= request.Brand,
                Price= request.Price,
                Stock= request.Stock
            });

            await _productRepository.SaveChangesAsync();

            return new CreateProductResponse() {  };
        }

        public async Task<GetAllProductsResponse> GetAllProducts()
        {
            var products = await _productRepository.GetByIdAsync("1");

            var resp = new GetAllProductsResponse() { Products = new List<Dtos.Base.ProductBase>() };
            return resp;
        }
    }
}
