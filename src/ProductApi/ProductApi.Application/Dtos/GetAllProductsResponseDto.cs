using ProductApi.Application.Dtos.Base;

namespace ProductApi.Application.Dtos
{
    public class GetAllProductsResponseDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
