using System;
using API.Data.Models;
using API.Data.ModelsData;
using API.Models.Country;
using API.Models.Hotel;
using API.Models.Users;
using AutoMapper;

namespace API.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Country, CreateCountryDto>().ReverseMap();
        CreateMap<Country, GetCountryDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Country, UpdateCountryDto>().ReverseMap();
        
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<Hotel, CreateHotelDto>().ReverseMap();

        CreateMap<UserDto, User>().ReverseMap();
    }
}
