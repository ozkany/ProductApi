using ProductApi.Application.Dtos;

namespace ProductApi.Application.Services
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateUser(CreateProductRequest request);

        Task<GetAllProductsResponse> GetAllProducts();
    }
}
