using AutoMapper;
using CountryInfoService.Models.Entities;
using ServiceReference;

namespace CountryInfoService.Core.Mapping;

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<tCountryInfo, Country>()
            .ForMember(dest => dest.ISOCode, opt => opt.MapFrom(src => src.sISOCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.sName))
            .ForMember(dest => dest.CapitalCity, opt => opt.MapFrom(src => src.sCapitalCity))
            .ForMember(dest => dest.PhoneCode, opt => opt.MapFrom(src => src.sPhoneCode))
            .ForMember(dest => dest.ContinentCode, opt => opt.MapFrom(src => src.sContinentCode))
            .ForMember(dest => dest.CurrencyISOCode, opt => opt.MapFrom(src => src.sCurrencyISOCode))
            .ForMember(dest => dest.CountryFlag, opt => opt.MapFrom(src => src.sCountryFlag));
    }
}