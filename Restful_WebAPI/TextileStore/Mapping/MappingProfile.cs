
namespace TextileStore.Mapping
{
    using AutoMapper;
    using TextileStore.Entity;
    using TextileStore.DTO;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, EditCategoryDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, EditProductDTO>().ReverseMap();
        }
    }
}