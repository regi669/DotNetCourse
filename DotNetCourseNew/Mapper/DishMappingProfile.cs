using AutoMapper;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Models;

namespace DotNetCourseNew.Mapper;

public class DishMappingProfile : Profile
{
    public DishMappingProfile()
    {
        CreateMap<CreateDishDTO, Dish>();
        CreateMap<Dish, DishDTO>();
    }
}