namespace CountryInfoService.Models.Dtos;

public record CountryResponseDto(
    int Id,
    string ISOCode,
    string Name,
    string CapitalCity,
    string PhoneCode,
    string ContinentCode,
    string CurrencyISOCode,
    string CountryFlag);