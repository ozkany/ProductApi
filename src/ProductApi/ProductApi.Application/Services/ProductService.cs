using AutoMapper;
using FluentValidation;
using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Base;
using ProductApi.Application.Validators;
using ProductApi.Domain.Exceptions;
using ProductApi.Domain.Interfaces;
using ProductApi.Domain.Models;
using System;
using System.Globalization;

namespace ProductApi.Application.Services
{
    public class ProductService : IProductService
    {
        IBaseRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        IValidator<ProductDto> _validator;

        public ProductService(IBaseRepository<Product> productRepository, IMapper mapper, IValidator<ProductDto> validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CreateProductResponseDto> CreateProductAsync(CreateProductRequestDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _validator.ValidateAsync(productDto, options => options
                .IncludeRuleSets("Create")
                .IncludeRulesNotInRuleSet()
                .ThrowOnFailures());

            await _productRepository.AddAsync(product);

            await _productRepository.SaveChangesAsync();

            return new CreateProductResponseDto() { Id = product.Id };
        }

        public async Task CreateProductRangeAsync(List<CreateProductRequestDto> productListDto)
        {
            var productList = _mapper.Map<IEnumerable<Product>>(productListDto);

            await _productRepository.AddRangeAsync(productList);

            await _productRepository.SaveChangesAsync();
        }

        public async Task<GetAllProductsResponseDto> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            var resp = new GetAllProductsResponseDto() { Products = productsDto };

            return resp;
        }

        public async Task<ProductDto> GetProductAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task UpdateProductAsync(UpdateProductRequestDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _productRepository.Update(product);

            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(string productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            _productRepository.Delete(product);

            await _productRepository.SaveChangesAsync();
        }
    }
}
