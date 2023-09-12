using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Base;
using ProductApi.Application.Mappings;
using ProductApi.Application.Services;
using ProductApi.Application.Validators;
using ProductApi.Domain.Interfaces;
using ProductApi.Domain.Models;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using System.Reflection;

namespace ProductApi.Application.Test
{
    public class ProductServiceTest
    {
        private readonly ProductService _productService;
        private readonly ProductDbContext _productDbContext;
        private readonly IBaseRepository<Product> _productRepository;
        ProductValidator _validator;
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public ProductServiceTest()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            _productDbContext = new ProductDbContext(options);
            _productRepository = new BaseRepository<Product>(_productDbContext);
            _validator = new ProductValidator(_productRepository);
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ProductMappingProfiles>());
            _mapper = mapperConfig.CreateMapper();
            _productService = new ProductService(_productRepository, _mapper, _validator);
        }

        [Fact]
        public async void Given_WithValidData_When_CreateProduct_Then_SuccessfullyCreateProduct()
        {
            // Arrange
            var productDto = new CreateProductRequestDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            var result = await _productService.CreateProductAsync(productDto);

            // Assert
            var addedProduct = await _productService.GetProductAsync(productDto.Id);
            Assert.Equal(result.Id, addedProduct.Id);
        }

        [Fact]
        public async void Given_WithValidData_When_DeleteProduct_Then_SuccessfullyDeleteProduct()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var productDto = new CreateProductRequestDto
            {
                Id = id,
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            await _productService.CreateProductAsync(productDto);

            // Assert
            var productExists = await _productService.GetProductAsync(id);
            Assert.NotNull(productExists);

            // Act
            await _productService.DeleteProductAsync(id);

            // Assert
            var productNull = await _productService.GetProductAsync(id);
            Assert.Null(productNull);
        }

        [Fact]
        public async void Given_WithValidData_When_UpdateProduct_Then_SuccessfullyUpdateProduct()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var productDto = new CreateProductRequestDto
            {
                Id = id,
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            await _productService.CreateProductAsync(productDto);

            // Assert
            var productExists = await _productService.GetProductAsync(id);
            Assert.NotNull(productExists);

            // Act
            var updateProductDto = new UpdateProductRequestDto()
            {
                Id = productDto.Id,
                Name = "UpdatedName",
                Brand = productDto.Brand,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock
            };

            _productDbContext.ChangeTracker.Clear();
            await _productService.UpdateProductAsync(updateProductDto);

            // Assert
            var updatedProduct = await _productService.GetProductAsync(id);
            Assert.Equal("UpdatedName", updatedProduct.Name);
        }
    }
}
