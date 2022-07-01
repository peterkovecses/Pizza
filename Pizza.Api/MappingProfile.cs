using AutoMapper;
using Pizza.Bll.Dtos;
using Pizza.Data.Entities;

namespace Pizza.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            
        }
    }
}
