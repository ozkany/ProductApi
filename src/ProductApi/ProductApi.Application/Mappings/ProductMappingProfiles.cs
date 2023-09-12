using AutoMapper;
using ProductApi.Application.Dtos.Base;
using ProductApi.Domain.Models;

namespace ProductApi.Application.Mappings
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
