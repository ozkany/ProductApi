using AutoMapper;
using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Base;
using ProductApi.Domain.Exceptions;
using ProductApi.Domain.Interfaces;
using ProductApi.Domain.Models;
using System.Globalization;

namespace ProductApi.Application.Services
{
    public class ProductService : IProductService
    {
        IBaseRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _productRepository.AddAsync(product);

            await _productRepository.SaveChangesAsync();

            return new CreateProductResponseDto() { Id = product.Id };
        }

        public async Task CreateProductRange(List<CreateProductRequestDto> productListDto)
        {
            var productList = _mapper.Map<IEnumerable<Product>>(productListDto);

            await _productRepository.AddRangeAsync(productList);

            await _productRepository.SaveChangesAsync();
        }

        public async Task<GetAllProductsResponseDto> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            var resp = new GetAllProductsResponseDto() { Products = productsDto };

            return resp;
        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task UpdateProduct(UpdateProductRequestDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _productRepository.Update(product);

            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProduct(string productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            _productRepository.Delete(product);

            await _productRepository.SaveChangesAsync();
        }
    }
}
