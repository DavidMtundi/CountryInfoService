using CountryInfoService.Models.Dtos;

namespace CountryInfoService.Core.Interfaces;

public interface ICountryService
{
    Task<CountryResponseDto?> GetAndSaveCountryInfoAsync(string countryName);
    Task<IEnumerable<CountryResponseDto>> GetAllCountriesAsync();
    Task<CountryResponseDto?> GetCountryByIdAsync(int id);
    Task UpdateCountryAsync(int id, CountryUpdateDto countryDto);
    Task DeleteCountryAsync(int id);
}