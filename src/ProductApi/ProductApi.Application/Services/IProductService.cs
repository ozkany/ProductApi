using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Base;

namespace ProductApi.Application.Services
{
    public interface IProductService
    {
        Task<GetAllProductsResponseDto> GetAllProductsAsync();

        Task<ProductDto> GetProductAsync(string id);

        Task<CreateProductResponseDto> CreateProductAsync(CreateProductRequestDto request);

        Task CreateProductRangeAsync(List<CreateProductRequestDto> productListDto);

        Task UpdateProductAsync(UpdateProductRequestDto productDto);

        Task DeleteProductAsync(string productId);
    }
}
