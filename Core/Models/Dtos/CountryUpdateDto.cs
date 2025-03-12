namespace CountryInfoService.Models.Dtos;

public class CountryUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string CapitalCity { get; set; } = string.Empty;
    public string PhoneCode { get; set; } = string.Empty;
    public string ContinentCode { get; set; } = string.Empty;
    public string CurrencyISOCode { get; set; } = string.Empty;
}