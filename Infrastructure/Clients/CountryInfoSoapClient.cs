using ServiceReference;

namespace CountryInfoService.Infrastructure.Clients;

public interface ICountryInfoSoapClient
{
    Task<string> GetCountryIsoCodeAsync(string countryName);
    Task<FullCountryInfoResponse> GetFullCountryInfoAsync(string isoCode);
}

public class CountryInfoSoapClient : ICountryInfoSoapClient
{
    private readonly CountryInfoServiceSoapTypeClient _client;
    private readonly ILogger<CountryInfoSoapClient> _logger;

    public CountryInfoSoapClient(
        CountryInfoServiceSoapTypeClient client,
        ILogger<CountryInfoSoapClient> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<string> GetCountryIsoCodeAsync(string countryName)
    {
        try
        {
            var response = await _client.CountryISOCodeAsync(countryName);
            return response.Body.CountryISOCodeResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SOAP call failed for country {CountryName}", countryName);
            throw;
        }
    }

    public async Task<FullCountryInfoResponse> GetFullCountryInfoAsync(string isoCode)
    {
        try
        {
            var response = await _client.FullCountryInfoAsync(isoCode);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SOAP call failed for ISO code {IsoCode}", isoCode);
            throw;
        }
    }
}