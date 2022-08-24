using AutoMapper;
using products.Domain.Itens.Entities;

namespace products.Domain.Itens.DTOs.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ItemDTO, Item>().ReverseMap();
    }
}