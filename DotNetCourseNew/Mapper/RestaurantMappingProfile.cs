using AutoMapper;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Models;

namespace DotNetCourseNew.Mapper
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<Dish, DishDTO>();
            CreateMap<Address, AddressDTO>();
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address
                    , c => c.MapFrom(dto => 
                        new Address(){ City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode}));
        }
    }
}