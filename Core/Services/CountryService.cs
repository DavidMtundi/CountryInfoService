using System.Globalization;
using CountryInfoService.Core.Interfaces;
using CountryInfoService.Infrastructure.Clients;
using CountryInfoService.Models.Dtos;
using CountryInfoService.Models.Entities;
using AutoMapper;

namespace CountryInfoService.Core.Services;

public class CountryService(
    IRepository<Country> repository,
    ICountryInfoSoapClient soapClient,
    IMapper mapper,
    ILogger<CountryService> logger)
    : ICountryService
{
    public async Task<CountryResponseDto?> GetAndSaveCountryInfoAsync(string countryName)
    {
        try
        {
            var normalizedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(countryName.ToLower());
            
            var isoCode = await soapClient.GetCountryIsoCodeAsync(normalizedName);
            if (string.IsNullOrEmpty(isoCode))
                throw new ArgumentException("Country not found");

            var countryInfo = await soapClient.GetFullCountryInfoAsync(isoCode);
            
            var countryEntity = mapper.Map<Country>(countryInfo);
            var createdEntity = await repository.AddAsync(countryEntity);
            
            return mapper.Map<CountryResponseDto>(createdEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving country info for {CountryName}", countryName);
            return null;
        }
    }

    public async Task<IEnumerable<CountryResponseDto>> GetAllCountriesAsync()
    {
        try
        {
            var countries = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<CountryResponseDto>>(countries);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving all countries");
            throw;
        }
    }

    public async Task<CountryResponseDto?> GetCountryByIdAsync(int id)
    {
        try
        {
            var country = await repository.GetByIdAsync(id);
            if (country == null)
                throw new KeyNotFoundException($"Country with ID {id} not found");

            return mapper.Map<CountryResponseDto>(country);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving country with ID {CountryId}", id);
            return null; //throw;
        }
    }

    public async Task UpdateCountryAsync(int id, CountryCreateDto countryDto)
    {
        try
        {
            var existingCountry = await repository.GetByIdAsync(id);
            if (existingCountry == null)
                throw new KeyNotFoundException($"Country with ID {id} not found");

            mapper.Map(countryDto, existingCountry);
            await repository.UpdateAsync(existingCountry);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating country with ID {CountryId}", id);
            throw;
        }
    }

    public async Task DeleteCountryAsync(int id)
    {
        try
        {
            var country = await repository.GetByIdAsync(id);
            if (country == null)
                throw new KeyNotFoundException($"Country with ID {id} not found");

            await repository.DeleteAsync(country);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting country with ID {CountryId}", id);
            throw;
        }
    }

}