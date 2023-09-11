using ProductApi.Application.Dtos;

namespace ProductApi.Application.Services
{
    public interface IProductService
    {
        Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto request);

        Task<GetAllProductsResponseDto> GetAllProducts();
    }
}
