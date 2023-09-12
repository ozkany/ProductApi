using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Interfaces;
using ProductApi.Domain.Models;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure.Test
{
    public class RepositoryTest
    {
        private readonly ProductDbContext _productDbContext;
        IBaseRepository<Product> _productRepository;

        public RepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            _productDbContext = new ProductDbContext(options);
            _productRepository = new BaseRepository<Product>(_productDbContext);
        }

        [Fact]
        public async void Given_ValidData_When_AddAsync_Then_SuccessfullyInsertData()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "iPhone 12",
                Brand= "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            var result = await _productDbContext.AddAsync(product);
            await _productDbContext.SaveChangesAsync();

            // Assert
            var addedProduct = _productDbContext.Products.First(p => p.Id == result.Entity.Id);
            Assert.Equal(result.Entity, addedProduct);
        }

        [Fact]
        public async void Given_ExistingProductId_When_AddAsync_Then_FailToInsertData()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var product1 = new Product
            {
                Id = id,
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            var product2 = new Product
            {
                Id = id,
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            await _productDbContext.AddAsync(product1);
            await _productDbContext.SaveChangesAsync();
            _productDbContext.ChangeTracker.Clear();

            await _productDbContext.AddAsync(product2);

            // Assert
            await Assert.ThrowsAnyAsync<ArgumentException>(async () => await _productDbContext.SaveChangesAsync());
        }

        [Fact]
        public async void Given_ValidData_When_AddAsync_FromRepository_Then_SuccessfullyInsertData()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            var result = await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            // Assert
            var addedProduct = await _productRepository.GetByIdAsync(result.Id);
            Assert.Equal(result, addedProduct);
        }

        [Fact]
        public async void Given_ExistingProductId_When_AddAsync_FromRepository_Then_FailToInsertData()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var product1 = new Product
            {
                Id = id,
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            var product2 = new Product
            {
                Id = id,
                Name = "iPhone 12",
                Brand = "Apple",
                Description = "Smartphone",
                Price = 590,
                Stock = 40
            };

            // Act
            await _productRepository.AddAsync(product1);
            await _productRepository.SaveChangesAsync();            

            // Assert
            await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await _productRepository.AddAsync(product2));
        }
    }
}
