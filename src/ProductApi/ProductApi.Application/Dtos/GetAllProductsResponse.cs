using ProductApi.Application.Dtos.Base;

namespace ProductApi.Application.Dtos
{
    public class GetAllProductsResponse
    {
        public List<ProductBase> Products { get; set; }
    }
}
