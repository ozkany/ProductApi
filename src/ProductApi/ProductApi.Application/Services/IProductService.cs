using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Base;

namespace ProductApi.Application.Services
{
    public interface IProductService
    {
        Task<GetAllProductsResponseDto> GetAllProducts();

        Task<ProductDto> GetProduct(string id);

        Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto request);

        Task CreateProductRange(List<CreateProductRequestDto> productListDto);
    }
}
