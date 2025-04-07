using AutoMapper;
using JwtApp.Back.Core.Application.Dto;
using JwtApp.Back.Core.Domain;

namespace JwtApp.Back.Core.Application.Mappings;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        this.CreateMap<Category, CategoriesListDto>().ReverseMap();
    }
}